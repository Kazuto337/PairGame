using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class CommunicationService : MonoBehaviour
{
    public GameDTO ReadGameJSON()
    {
        string path = Application.streamingAssetsPath + "PairsGameJSON.text";

        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            GameDTO gameData = JsonUtility.FromJson<GameDTO>(json);

            return gameData;
        }

        return null;
    }
}
