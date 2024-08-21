using System.IO;
using UnityEngine;
using Entities;

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

                return gameData;
            }

            return null;
        }
    } 
}
