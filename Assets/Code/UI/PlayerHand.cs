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
		[SerializeField]
		private CardFace _cardFacePrefab;
		[SerializeField]
		private CardVisualSet _cardVisualSet;

		private List<CardFace> _cards = new List<CardFace>();

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
				CardFace cardFace = Instantiate(_cardFacePrefab, cardPivot.transform);
				cardFace.SetFace(card, _cardVisualSet);
				
				_cards.Add(cardFace);
			}
		}

		[Button]
		private void Reorder()
		{
			int totalCards = _cards.Count;

			float firstCardAngle = _cardRotation * totalCards / 2;
			
			for (int i = 0; i < totalCards; i++)
			{
				CardFace cardFace = _cards[i];

				Transform pivot = cardFace.transform.parent;
				
				pivot.localPosition = new Vector3(0, -_rotationPivotDistance, 0);
				cardFace.transform.localPosition = new Vector3(0, _rotationPivotDistance, 0);

				Quaternion rotation = Quaternion.Euler(0, 0, firstCardAngle - (i * _cardRotation));
				
				pivot.localRotation = rotation;

				cardFace.OrderInLayer = i;
			}
		}
	}
}