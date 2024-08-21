using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using Entities;
using UnityEngine.SocialPlatforms.Impl;

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
        }

        private void OnGameFinished()
        {
            ResultsDTO stats = level.GetStatistics();

            string totalClicks = stats.total_clicks.ToString();
            string totalTime = stats.total_time.ToString(); 
            string pairs = stats.pairs.ToString();
            string score = stats.score.ToString();

            ui_Manager.ShowWinnerPanel(totalClicks , totalTime , pairs , score);
        }

        public void SaveStatistics()
        {
            ResultsDTO stats = level.GetStatistics();
        }
    } 
}
