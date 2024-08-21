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
        [Header("Game Initialization")]
        [SerializeField] CommunicationService commService;
        [SerializeField] LevelCreator levelCreator;

        [Header("UI")]
        [SerializeField] UI_Manager ui_Manager;

        [Header("Gameplay")]
        LevelBehaviour level;

        private void Awake()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            GameDTO gameData = commService.ReadGameJSON();
            level = levelCreator.CreateLevel(gameData.blocks);
            level.OnWinnerDetected.AddListener(OnGameFinished);

            level.username = GlobalObjects.instance.GetUsername();
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
                ResultsDTO stats = level.GetStatistics();
                BinaryFormatter formatter = new BinaryFormatter();
                string path = Application.streamingAssetsPath + "/stats.k337";

                FileStream stream = new FileStream(path, FileMode.CreateNew);

                formatter.Serialize(stream, stats);
                stream.Close();
            }
            catch (Exception error)
            {
                ui_Manager.SendFeedBackMessage("An error has occured While Saving D:");
                throw new Exception($"An error has occurred: {error}");
            }
        }
    } 
}
