using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using Entities;

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
        }
    } 
}
