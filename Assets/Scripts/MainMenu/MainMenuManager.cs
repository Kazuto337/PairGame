using Core;
using Entities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] CommunicationService commsService;

        [SerializeField] TMP_InputField usernameField;
        [SerializeField] GlobalObjects globalObjects;
        [SerializeField] ScenesManager scenesManager;
        [SerializeField] int buildIndexGameScene;

        [Header("Scoreboard")]
        [SerializeField] ScoreBoardBehaviour scoreBoardPanel;
        [SerializeField] GameObject scoreBoardButton;

        private void Start()
        {
            globalObjects = GlobalObjects.instance;
        }
        private void OnEnable()
        {
            GetScoreBoard();
        }

        public void SaveUserName()
        {
            if (usernameField.text == string.Empty || usernameField == null)
            {
                globalObjects.SetUsername("DEFAULT");
                scenesManager.LoadScene(buildIndexGameScene);

                return;
            }

            globalObjects.SetUsername(usernameField.text);
            Play();
        }

        private void Play()
        {
            scenesManager.LoadScene(buildIndexGameScene);
        }

        private void GetScoreBoard()
        {
            ScoreBoardDTO loadedScoreboard = commsService.ReadScoreBoardJSON();

            if (loadedScoreboard == null)
            {
                scoreBoardButton.SetActive(false);
                return;
            }

            scoreBoardButton.SetActive(true);
            scoreBoardPanel.LoadScoreboard(loadedScoreboard);
        }

        public void OpenScoreBoard()
        {
            scoreBoardPanel.gameObject.SetActive(true);
        }
        public void CloseScoreBoard()
        {
            scoreBoardPanel.gameObject.SetActive(false);
        }
    }
}
