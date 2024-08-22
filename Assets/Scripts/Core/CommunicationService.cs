using System.IO;
using UnityEngine;
using Entities;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices.ComTypes;

namespace Core
{
    public class CommunicationService : MonoBehaviour
    {
        public GameDTO ReadGameJSON()
        {
            string path = Application.streamingAssetsPath + "/PairsGameJSON.txt";

            if (File.Exists(path))
            {

                string json = File.ReadAllText(path);
                GameDTO gameData = JsonUtility.FromJson<GameDTO>(json);

                if (CheckGameData(gameData))
                {
                    return gameData;
                }

                return null;                
            }

            return null;
        }

        public ScoreBoardDTO ReadScoreBoardJSON()
        {
            string path = Application.streamingAssetsPath + "/stats.k337";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                ScoreBoardDTO gameStats = formatter.Deserialize(stream) as ScoreBoardDTO;

                stream.Close();
                
                return gameStats;
            }

            Debug.LogWarning("Scoreboard File not found");
            return null;
        }

        private bool CheckGameData(GameDTO gameData)
        {
            List<BlockData> blocks = gameData.blocks;

            int rows = 0;
            int colums = 0;

            bool conditionA = (rows < 2 || rows > 8);
            bool conditionB = (colums < 2 || colums > 8);

            for (int i = 0; i < blocks.Count; i++)
            {
                if (rows < blocks[i].R)
                {
                    rows = blocks[i].R;
                }
            }

            for (int i = 0; i < blocks.Count; i++)
            {
                if (rows < blocks[i].C)
                {
                    rows = blocks[i].C;
                }
            }

            if (conditionA || conditionB)
            {
                return false;
            }

            return true;
        }
    }
}
