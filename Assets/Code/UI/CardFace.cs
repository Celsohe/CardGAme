using System.Collections;
using Code.Cards;
using Code.UI.Selection;
using UnityEngine;

namespace Code.UI
{
	public sealed class CardFace : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _face;

		private Card _card;

		public int OrderInLayer
		{
			get
			{
				return _face.sortingOrder;
			}
			set
			{
				_face.sortingOrder = value;
			}
		}

		public Card Card
		{
			get
			{
				return _card;
			}
		}
		
		public void SetFace(Card card, CardVisualSet visualSet)
		{
			_card = card;
			_face.sprite = visualSet.GetCardSprite(card);
		}
	}
}