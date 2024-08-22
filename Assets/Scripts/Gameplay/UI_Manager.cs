using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class UI_Manager : MonoBehaviour
    {
        [Header("Gameplay Panel")]
        [SerializeField] GameObject gameplayPanel;

        [Header("Statistics Panel")]
        [SerializeField] GameObject winnerPanel;

        [SerializeField] TMP_Text totalClicksTMP;
        [SerializeField] TMP_Text totalTimeTMP;
        [SerializeField] TMP_Text pairTMP;
        [SerializeField] TMP_Text scoreTMP;

        [Header("Feedback")]
        [SerializeField] TMP_Text feedbackTMP;

        private void Start()
        {
            winnerPanel.SetActive(false);
        }
        public void ShowWinnerPanel(string totalClicks , string totalTime, string pairs , string score)
        {
            totalClicksTMP.text = totalClicks;
            totalTimeTMP.text = $"{totalTime} seconds";
            pairTMP.text = pairs;
            scoreTMP.text = $"{score} points";

            winnerPanel.SetActive(true);

            gameplayPanel.SetActive(false);
        }

        public void SendFeedBackMessage(string message , Color color)
        {
            feedbackTMP.text = message;
            feedbackTMP.color = color;
            feedbackTMP.gameObject.SetActive(true);

            StartCoroutine(FeedbackMessageBehaviour());
        }

        private IEnumerator FeedbackMessageBehaviour()
        {
            yield return new WaitForSeconds(3f);
            feedbackTMP.text = string.Empty;
            feedbackTMP.gameObject.SetActive(false);
        }
    } 
}
