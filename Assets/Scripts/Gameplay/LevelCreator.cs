using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] BlockFabric blockFabric;

        [SerializeField] Transform parentTransform;

        public LevelBehaviour CreateLevel(List<BlockData> blocks)
        {
            GameObject newLevelObj = new GameObject("Level");
            newLevelObj.transform.SetParent(parentTransform , false);

            LevelBehaviour newLevel = newLevelObj.AddComponent<LevelBehaviour>();

            BlockBehaviour[,] levelMatrix = CreateMatrix(blocks , newLevelObj.gameObject.transform);
            newLevel.Construct(levelMatrix);

            return newLevel;
        }

        private BlockBehaviour[,] CreateMatrix(List<BlockData> blocks , Transform blockParent)
        {
            int rows = GetMatrixRows(blocks);
            int colums = GetMatrixColums(blocks);

            BlockBehaviour[,] newMatrix = new BlockBehaviour[rows, colums];

            foreach (BlockData block in blocks)
            {
                BlockBehaviour newBlockObj = blockFabric.CreateBlock(block , blockParent);
                newMatrix[newBlockObj.Coords.x-1, newBlockObj.Coords.y-1] = newBlockObj;
            }

            return newMatrix;
        }

        private int GetMatrixRows(List<BlockData> blocks)
        {
            int greaterRowValue = 0;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (greaterRowValue < blocks[i].R)
                {
                    greaterRowValue = blocks[i].R;
                }
            }

            return greaterRowValue;
        }
        private int GetMatrixColums(List<BlockData> blocks)
        {
            int greaterColumValue = 0;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (greaterColumValue < blocks[i].R)
                {
                    greaterColumValue = blocks[i].R;
                }
            }

            return greaterColumValue;
        }
    }
}
