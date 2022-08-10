using System;
using System.Collections.Generic;
using BitStrap;
using Code.Cards;
using Code.Game;
using Code.Turn;
using UnityEngine;

namespace Code.UI
{
	public sealed class PlayerHand : MonoBehaviour
	{
		[SerializeField]
		private Player.Index _playerIndex;
		[SerializeField]
		private Transform _cardsParent;
		[SerializeField]
		private float _cardRotation = 15;
		[SerializeField]
		private float _rotationPivotDistance = 10f;
		[Header("Project References")]
		[SerializeField]
		private SelectableCard _selectableCardPrefab;
		[SerializeField]
		private CardVisualSet _cardVisualSet;

		private List<SelectableCard> _cards = new List<SelectableCard>();
		
		public void OnEnable()
		{
			CardGiver.OnFinshedGivingCards += ShowPlayerCards;
		}

		public void OnDisable()
		{
			CardGiver.OnFinshedGivingCards -= ShowPlayerCards;
		}

        private void ShowPlayerCards()
		{
			InstantiateCards();
			Reorder();
		}

		private void InstantiateCards()
		{
			CardSet playerCards = PlayerController.Instance.GetPlayer(_playerIndex).Cards;//TurnController.Instance.CurrentPlayerIndex).Cards;
			IReadOnlyList<Card> cards = playerCards.Cards;
			int totalCards = cards.Count;
			
			for (int i = 0; i < totalCards; i++)
			{
				GameObject cardPivot = new GameObject("CardPivot");
				cardPivot.transform.SetParent(_cardsParent, false);
				Card card = cards[i];
				SelectableCard selectableCard = Instantiate(_selectableCardPrefab, cardPivot.transform);
				selectableCard.GetComponent<CardFace>().SetFace(card, _cardVisualSet);
				selectableCard.SetHolder(this);
				
				_cards.Add(selectableCard);
			}
		}

		[Button]
		private void Reorder()
		{
			int totalCards = _cards.Count;

			float firstCardAngle = _cardRotation * totalCards / 2;
			
			for (int i = 0; i < totalCards; i++)
			{
				SelectableCard selectableCard = _cards[i];

				Transform pivot = selectableCard.transform.parent;
				
				pivot.localPosition = new Vector3(0, -_rotationPivotDistance, 0);
				selectableCard.transform.localPosition = new Vector3(0, _rotationPivotDistance, 0);

				Quaternion rotation = Quaternion.Euler(0, 0, firstCardAngle - (i * _cardRotation));
				
				pivot.localRotation = rotation;

				selectableCard.GetComponent<CardFace>().OrderInLayer = i;
			}
		}
	}
}