using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class LevelBehaviour : MonoBehaviour
    {
        UnityEvent onWinnerDetected = new UnityEvent();
        BlockBehaviour[,] levelMatrix;
        [SerializeField] GameStatisticManager statisticManager;

        BlockBehaviour selectedBlock1, selectedBlock2;
        int pairs2Win = 0;
        bool isVerifyingPairs = false;

        public UnityEvent OnWinnerDetected { get => onWinnerDetected;}

        private void Start()
        {
            SetStatistics();
        }
        public void Construct(BlockBehaviour[,] matrix)
        {
            levelMatrix = matrix;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j].OnClicked.AddListener(SelectBlock);
                }
            }

            CalculatePairs2Win();
        }

        private void SetStatistics()
        {
            statisticManager = GameStatisticManager.Instance;
            statisticManager.StartGameTime();
            onWinnerDetected.AddListener(statisticManager.StopGameTime);
        }

        private void CalculatePairs2Win()
        {
            List<int> gameElements = new List<int>();            
            for (int i = 0; i < levelMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < levelMatrix.GetLength(1); j++)
                {
                    gameElements.Add(levelMatrix[i,j].NumberID);
                }
            }

            for (int i = 0; i < gameElements.Count; i++)
            {
                for (int j = i+1; j < gameElements.Count; j++)
                {
                    if (gameElements[i] == gameElements[j])
                    {
                        pairs2Win++;
                    }
                }
            }

            Debug.LogWarning($"Pairs to win: {pairs2Win}");
        }

        public void SortBlocks(Vector3 initialBlockPosition, float spacingX, float spacingY)
        {
            Vector3 newPosition = initialBlockPosition;

            for (int i = 0; i < levelMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < levelMatrix.GetLength(1); j++)
                {
                    if (levelMatrix[i, j] != null)
                    {
                        levelMatrix[i, j].transform.position = newPosition;
                        newPosition.x += spacingX;
                    }
                }

                newPosition.x = initialBlockPosition.x;
                newPosition.y -= spacingY;
            }
        }

        private void SelectBlock(BlockBehaviour selectedBlock)
        {
            if (isVerifyingPairs)
            {
                return;
            }

            statisticManager.AddClick();
            if (selectedBlock2 != null)
            {
                ClearSelectedBlocks();
                return;
            }

            if (selectedBlock1 != null)
            {
                selectedBlock2 = selectedBlock;
                selectedBlock2.ShowIcon();

                StartCoroutine(VerifyPairs());
                return;
            }

            selectedBlock1 = selectedBlock;
            selectedBlock1.ShowIcon();
        }

        private IEnumerator VerifyPairs()
        {
            isVerifyingPairs = true;
            yield return new WaitForSeconds(.5f);

            if (selectedBlock1.NumberID != selectedBlock2.NumberID)
            {
                selectedBlock1.HideIcon();
                selectedBlock2.HideIcon();

                ClearSelectedBlocks();
                isVerifyingPairs = false;

                yield break;
            }

            selectedBlock1.DisableBlockInteraction();
            selectedBlock2.DisableBlockInteraction();

            statisticManager.AddPair();
            ClearSelectedBlocks();
            VerifyWinner();

            isVerifyingPairs = false;

            yield return null;
        }

        private void VerifyWinner()
        {
            if (statisticManager.Pairs == pairs2Win)
            {
                Debug.LogWarning("Winner");
                onWinnerDetected.Invoke();
                return;
            }
        }

        private void ClearSelectedBlocks()
        {
            selectedBlock1 = null;
            selectedBlock2 = null;
        }

        public ResultsDTO GetStatistics()
        {
            return statisticManager.GetCurrentResults();
        }
    }
}
