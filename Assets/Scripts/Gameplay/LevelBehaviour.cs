using Entities;
using System.Collections;
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
            statisticManager = GameStatisticManager.Instance;
            onWinnerDetected.AddListener(statisticManager.StopGameTime);
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

        private void CalculatePairs2Win()
        {
            pairs2Win = 0;

            for (int i = 0; i < levelMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < levelMatrix.GetLength(1) - 1; j++)
                {
                    if (levelMatrix[i, j] == levelMatrix[i, j + 1])
                    {
                        pairs2Win++;
                    }
                }
            }

            for (int j = 0; j < levelMatrix.GetLength(1); j++)
            {
                for (int i = 0; i < levelMatrix.GetLength(0) - 1; i++)
                {
                    if (levelMatrix[i, j] == levelMatrix[i + 1, j])
                    {
                        pairs2Win++;
                    }
                }
            }
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
            yield return new WaitForSeconds(1.5f);

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
