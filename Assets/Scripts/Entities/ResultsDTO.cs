using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [System.Serializable]
	public class ResultsDTO
	{
        public string username;

        public int total_clicks;
        public float total_time;
        public int pairs;
        public int score;
    }

    [System.Serializable]
    public class ScoreBoardDTO
    {
        public List<ResultsDTO> results = new List<ResultsDTO>();
    }
}
