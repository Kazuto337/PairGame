using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
	public class BlockBehaviour : MonoBehaviour
	{
		[SerializeField] Image _icon;
		[SerializeField] bool _show = true;

		Vector2Int _coords;
		int _numberID;

		public Vector2Int Coords { get => _coords; }
		public int NumberID { get => _numberID; }

		public void Constructor(Vector2Int coords , int numberID , Sprite iconSprite)
		{
			_coords = coords;
			_numberID = numberID;

			_icon.sprite = iconSprite;
			_show = false;
		}

        private void Update()
        {
			_icon.enabled = _show;
        }
    } 
}
