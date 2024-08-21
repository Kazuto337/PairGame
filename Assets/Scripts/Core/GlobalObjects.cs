using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GlobalObjects : MonoBehaviour
    {
        public static GlobalObjects instance;
        private static string username;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else instance = this;

            DontDestroyOnLoad(gameObject);
        }
        public string GetUsername()
        {
            return username;
        }

        public void SetUsername(string username)
        {
            ModifyUsername(username);
        }

        private static void ModifyUsername(string newUsername)
        {
            username = newUsername;
        }
    }
}
