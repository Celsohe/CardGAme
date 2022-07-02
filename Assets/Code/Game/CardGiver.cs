using System;
using Code.Cards;
using UnityEngine;

namespace Code.Game
{
	public sealed class CardGiver : MonoBehaviour
	{
		public delegate void FinshedGivingCards();
		public static event FinshedGivingCards OnFinshedGivingCards;
		
		private void OnEnable()
		{
			GameController.OnGameStateChanged += OnGameStageChanged;
		}
		
		private void OnDisable()
		{
			GameController.OnGameStateChanged -= OnGameStageChanged;
		}
		
		private void OnGameStageChanged(GameController.GameStage state)
		{
			if (state == GameController.GameStage.DistributeCards)
			{
				GiveCards();
			}
		}

		private void GiveCards()
		{
			CardSet deck = new CardSet();
			deck.FillDeck();
			for(int i = 0; i < 10; i++)
			{
				PlayerController.Instance.GetPlayer(Player.Index.Player1).Cards.AddCard(deck.GetRandom());
				PlayerController.Instance.GetPlayer(Player.Index.Player2).Cards.AddCard(deck.GetRandom());
			}
			
			if(OnFinshedGivingCards != null)
			{
				OnFinshedGivingCards();
			}
		}
	}
}