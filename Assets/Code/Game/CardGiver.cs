using System.Collections.Generic;
using Code.Cards;
using Code.UI;
using UnityEngine;

namespace Code.Game
{
	public sealed class CardGiver : MonoBehaviour
	{
		/// <summary>
		/// Delegate 
		/// </summary>
		public delegate void FinshedGivingCards();
		public static event FinshedGivingCards OnFinshedGivingCards;
		[SerializeField] int numCard;

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

			IReadOnlyList<PlayerHand> playerHands = PlayerHand.All;

			for(int i = 0; i < numCard; i++)
			{
				for (int j = 0; j < playerHands.Count; j++)
				{
					playerHands[j].AddCard(deck.GetRandom());
				}
			}
			
			if(OnFinshedGivingCards != null)
			{
				OnFinshedGivingCards();
			}
		}
	}
}