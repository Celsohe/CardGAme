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
		private float _cardDistance = 2;
		[SerializeField]
		private float _cardRotation = 5;
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
				Card card = cards[i];
				CardFace cardFace = Instantiate(_cardFacePrefab, _cardsParent);
				cardFace.SetFace(card, _cardVisualSet);
				
				_cards.Add(cardFace);
			}
		}

		[Button]
		private void Reorder()
		{
			int totalCards = _cards.Count;
			
			float firstCardXPosition = -(_cardDistance / 2f) * totalCards + _cardDistance;
			
			for (int i = 0; i < totalCards; i++)
			{
				CardFace cardFace = _cards[i];
				
				float xPosition = firstCardXPosition + (i * _cardDistance);
				
				cardFace.transform.localPosition = new Vector3(xPosition, 0, 0);
				cardFace.transform.Rotate(0, 0, 10 - (i+1) * 2);
				//cardFace.transform.localPosition = new Vector3(xPosition, .8f*(float)Math.Sin((180-((i+1) * 18))*(Math.PI)/180), 0);//.8f*(float)(Math.Sin(((i+1)*18))*(Math.PI/180)));
				//cardFace.transform.Rotate(0, -30 + (i+1) * 6, 10 - (i+1) * 2); //Rotacionar as cartas cardFace.transform.Rotate(0, -80 + i * 16, 40 - i * 8)

				cardFace.OrderInLayer = i;
			}
		}
	}
}