using System.Collections.Generic;
using Code.Cards;
using Code.UI.Selection;
using UnityEngine;

namespace Code.UI
{
	public sealed class CardPileFace : MonoBehaviour, ISelectable, IInteractable
	{
		[SerializeField]
		private CardFace _cardFacePrefab;
		[SerializeField]
		private CardVisualSet _cardVisualSet;
		[Header("Presentation")]
		[SerializeField]
		private float _cardDistance = .2f;
		
		private CardPile _cardPile = new CardPile();
		private List<CardFace> _cardFaces = new List<CardFace>();
		
		public void Select()
		{
			// TODO
		}

		public void Unselect()
		{
			// TODO
		}

		public bool Interact(ISelectable selectedObject)
		{
			if (selectedObject is SelectableCard)
			{
				SelectableCard selectedCard = (SelectableCard) selectedObject;
				AddCardToPile(selectedCard.GetComponent<CardFace>().Card);
				selectedCard.RemoveSelfFromHand();
				return true;
			}
			return false;
		}
		
		public void AddCardToPile(Card card)
		{
			_cardPile.Add(card);
			
			CardFace cardFace = Instantiate(_cardFacePrefab, transform);
			cardFace.SetFace(card, _cardVisualSet);
			cardFace.OrderInLayer = _cardFaces.Count;
			_cardFaces.Add(cardFace);

			Refresh();
		}
		
		public Card RemoveCardFromPile()
		{
			// TODO
			return null;
		}

		private void Refresh()
		{
			for (int i = 0; i < _cardFaces.Count; i++)
			{
				Vector3 cardPosition = _cardFaces[i].transform.localPosition;
				cardPosition.y = -_cardDistance * i;
				_cardFaces[i].transform.localPosition = cardPosition;
				_cardFaces[i].OrderInLayer = i;
			}
		}
	}
}