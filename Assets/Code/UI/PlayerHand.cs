using System;
using System.Collections.Generic;
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
		private CardFace _cardFacePrefab;
		[SerializeField]
		private CardVisualSet _cardVisualSet;

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
			CardSet playerCards = PlayerController.Instance.GetPlayer(_playerIndex).Cards;//TurnController.Instance.CurrentPlayerIndex).Cards;
			IReadOnlyList<Card> cards = playerCards.Cards;
			float firstCardXPosition = 0;
			int totalCards = cards.Count;
			if (totalCards % 2 == 0)
			{
				firstCardXPosition = -((totalCards - 1) / 2f) * _cardDistance;
			}
			else
			{
				firstCardXPosition = (totalCards / 2f) * _cardDistance;
			}
			
			for (int i = 0; i < totalCards; i++)
			{
				Card card = cards[i];
				CardFace cardFace = Instantiate(_cardFacePrefab, _cardsParent);
				cardFace.SetFace(card, _cardVisualSet);
				float xPosition = firstCardXPosition + i * _cardDistance;
				cardFace.transform.localPosition = new Vector3(xPosition, 0, 0);
				cardFace.transform.Rotate(0,-30+i*9, 45 - i * 9);
				cardFace.OrderInLayer = i;
			}

			





    }
	}


}