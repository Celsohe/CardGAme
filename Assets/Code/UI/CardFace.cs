using Code.Cards;
using UnityEngine;

namespace Code.UI
{
	[RequireComponent(typeof(BoxCollider))]
	public sealed class CardFace : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _face;
		[SerializeField]
		private BoxCollider _boxCollider = null;

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

		public float Width
		{
			get
			{
				return _boxCollider.size.x;
			}
		}

		public float Height
		{
			get
			{
				return _boxCollider.size.y;
			}
		}
		
		public void SetFace(Card card, CardVisualSet visualSet)
		{
			_face.sprite = visualSet.GetCardSprite(card);
		}
	}
}