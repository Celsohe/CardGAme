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
		[SerializeField]
		private GameObject Player1Hand;
		[SerializeField]
		private GameObject Player2Hand;


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
				cardFace.transform.localPosition = new Vector3(xPosition +i, .8f*(float)Math.Sin((180-((i+1) * 18))*(Math.PI)/180), .8f*(float)(Math.Sin(((i+1)*18))*(Math.PI/180)));
				cardFace.transform.Rotate(0, -30 + (i+1) * 6, 10 - (i+1) * 2); //Rotacionar as cartas cardFace.transform.Rotate(0, -80 + i * 16, 40 - i * 8)
				Player1Hand.transform.localPosition = new Vector3(-4.5f, 9, -14);
				Player2Hand.transform.localPosition = new Vector3(-5, 11, 0);
				Player2Hand.transform.localScale = new Vector3(.5f,.5f,.5f);

				cardFace.OrderInLayer = i;
			}

			





    }
	}


}