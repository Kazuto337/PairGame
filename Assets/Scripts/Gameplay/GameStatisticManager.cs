using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class GameStatisticManager : MonoBehaviour
    {
        public static GameStatisticManager Instance;

        int total_clicks;
        float total_time;
        int pairs;
        int score;

        private bool isGameRunning;

        public int Total_clicks { get => total_clicks; }
        public float Total_time { get => total_time; }
        public int Pairs { get => pairs; }
        public int Score { get => score; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else Instance = this;
        }


        private void Update()
        {
            if (isGameRunning)
            {
                total_time += Time.deltaTime;
            }
        }

        public void AddClick()
        {
            total_clicks++;
        }
        public void AddPair()
        {
            pairs++;
        }

        public void StopGameTime()
        {
            isGameRunning = false;
        }

        public ResultsDTO GetCurrentResults()
        {
            ResultsDTO newResultsDTO = new ResultsDTO();
            newResultsDTO.total_clicks = total_clicks;
            newResultsDTO.total_time = total_time;
            newResultsDTO.pairs = pairs;
            newResultsDTO.score = score;

            return newResultsDTO;
        }
    }
}
