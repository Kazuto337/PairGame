using Core;
using Entities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class ScoreBoardBehaviour : MonoBehaviour
    {
        [SerializeField] List<TMP_Text> usernames, scores;

        public void LoadScoreboard(ScoreBoardDTO loadedScoreboard)
        {
            List<ResultsDTO> results = SortScoreBoard(loadedScoreboard);

            if (usernames.Count < results.Count)
            {
                for (int i = 0; i < usernames.Count; i++)
                {
                    usernames[i].gameObject.SetActive(true);
                    scores[i].gameObject.SetActive(true);
                }

                for (int i = 0; i < usernames.Count; i++) // It only shows the greater values
                {
                    usernames[i].text = results[i].username;
                    scores[i].text = $"{results[i].score} pts";
                }

                return;
            }


            for (int i = results.Count; i < usernames.Count; i++)
            {
                usernames[i].gameObject.SetActive(false);
                scores[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < results.Count; i++)
            {
                usernames[i].text = results[i].username;
                scores[i].text = $"{results[i].score} pts";
            }
        }

        private List<ResultsDTO> SortScoreBoard(ScoreBoardDTO scoreBoard)
        {
            List<ResultsDTO> results = scoreBoard.results;
            ResultsDTO temp = null;
            for (int i = 0; i < results.Count; i++)
            {
                for (int j = 0; j < results.Count - 1; j++)
                {
                    if (results[i].score > results[j + 1].score)
                    {
                        temp = results[j + 1];
                        results[j + 1] = results[i];
                        results[i] = temp;
                    }
                }
            }

            return results;
        }
    }
}
