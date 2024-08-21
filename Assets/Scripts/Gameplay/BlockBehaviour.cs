using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay
{
	public class BlockBehaviour : MonoBehaviour
	{
		[SerializeField] Image _icon , background;
		[SerializeField] Button _button;
		[SerializeField] bool _show = true;

		UnityEvent<BlockBehaviour> onClicked = new UnityEvent<BlockBehaviour>();

		Vector2Int _coords;
		int _numberID;

		public Vector2Int Coords { get => _coords; }
		public int NumberID { get => _numberID; }
        public UnityEvent<BlockBehaviour> OnClicked { get => onClicked;}

        public void Constructor(Vector2Int coords, int numberID, Sprite iconSprite)
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

		public void Interact()
		{
			onClicked?.Invoke(this);
		}
		public void ShowIcon()
		{
			_show = true;
		}
		public void HideIcon()
		{
            _show = false;
        }

		public void DisableBlockInteraction()
		{
			background.color = Color.gray;
			_button.interactable = false;
		}
    } 
}
