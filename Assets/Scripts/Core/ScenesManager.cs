using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class ScenesManager : MonoBehaviour
    {
        public void LoadScene(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    } 
}
