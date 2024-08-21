using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameStatisticManager : MonoBehaviour
    {
        public static GameStatisticManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else Instance = this;
        }
    }
}
