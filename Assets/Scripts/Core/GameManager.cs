using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using Entities;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

namespace Core
{    
    public class GameManager : MonoBehaviour
    {
        [Header("Game System")]
        [SerializeField] ScenesManager scenesManager;

        [Header("Game Initialization")]
        [SerializeField] CommunicationService commService;
        [SerializeField] LevelCreator levelCreator;

        [Header("UI")]
        [SerializeField] UI_Manager ui_Manager;

        [Header("Gameplay")]
        LevelBehaviour level;

        private void OnEnable()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            GameDTO gameData = commService.ReadGameJSON();

            if (gameData == null)
            {
                StartCoroutine(GameFailed());
                return;
            }

            level = levelCreator.CreateLevel(gameData.blocks);
            level.OnWinnerDetected.AddListener(OnGameFinished);

            level.username = GlobalObjects.instance.GetUsername();
        }

        private IEnumerator GameFailed()
        {
            ui_Manager.SendFeedBackMessage("Something went wrong >:/   Please Check the GameData JSON", Color.red);

            yield return new WaitForSeconds(3f);

            scenesManager.LoadScene(0); //Load Menu
        }

        private void OnGameFinished()
        {
            ResultsDTO stats = level.GetStatistics();

            string totalClicks = stats.total_clicks.ToString();
            string totalTime = stats.total_time.ToString("N"); 
            string pairs = stats.pairs.ToString();
            string score = stats.score.ToString();

            ui_Manager.ShowWinnerPanel(totalClicks , totalTime , pairs , score);
        }

        public void SaveStatistics()
        {
            try
            {
                ScoreBoardDTO scoreboard;

                BinaryFormatter formatter = new BinaryFormatter();
                string path = Application.streamingAssetsPath + "/stats.k337";

                FileStream stream;

                if (File.Exists(path))
                {
                    stream = new FileStream(path, FileMode.Open);

                    scoreboard = formatter.Deserialize(stream) as ScoreBoardDTO;
                    stream.Close();
                }
                else
                {
                    scoreboard = new ScoreBoardDTO();
                }

                ResultsDTO stats = level.GetStatistics();
                stream = new FileStream(path, FileMode.Create);

                scoreboard.results.Add(stats);

                formatter.Serialize(stream, scoreboard);
                stream.Close();

                ui_Manager.SendFeedBackMessage("Statistics saved successfully! :D" , Color.green);
            }
            catch (Exception error)
            {
                ui_Manager.SendFeedBackMessage("An error has occured While Saving D:" , Color.red);
                throw new Exception($"An error has occurred: {error}");
            }
        }
    } 
}
