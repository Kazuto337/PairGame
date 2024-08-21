using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        [SerializeField] TMP_InputField usernameField;
        [SerializeField] GlobalObjects globalObjects;
        [SerializeField] ScenesManager scenesManager;
        [SerializeField] int buildIndexGameScene;

        [Header("Scoreboard")]
        [SerializeField] GameObject scoreBoardPanel;

        private void Start()
        {
            globalObjects = GlobalObjects.instance;
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
            scenesManager.LoadScene(buildIndexGameScene);
        }

        public void OpenScoreBoard()
        {

        }
        public void CloseScoreBoard()
        {

        }
    } 
}
