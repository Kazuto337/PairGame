using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LevelBehaviour : MonoBehaviour
    {
        BlockBehaviour[,] levelMatrix;
        [SerializeField] GameStatisticManager statisticManager;

        private void Start()
        {
            statisticManager = GameStatisticManager.Instance;
        }
        public void Construct(BlockBehaviour[,] matrix)
        {
            levelMatrix = matrix;
        }
    } 
}
