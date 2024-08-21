using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BlockFabric : MonoBehaviour
    {
        [SerializeField] GameObject blockPrefab;
        [SerializeField] List<Sprite> icons;

        public BlockBehaviour CreateBlock(BlockData block, Transform parentTransform)
        {
            BlockBehaviour newBlockObj = Instantiate(blockPrefab, parentTransform).GetComponent<BlockBehaviour>();

            Vector2Int newBlockCoords = new Vector2Int(block.R, block.C);
            newBlockObj.Constructor(newBlockCoords, block.number, icons[block.number-1]);

            return newBlockObj;
        }
    }
}
