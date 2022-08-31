using System;
using System.Collections.Generic;
using Code.Game;
using Code.Turn;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
	public sealed class EndTurnButton : MonoBehaviour
	{
		[SerializeField]
		private Button _button = null;

		// O botão deve ser habilitado:
		// - Na fase de construção dos montes:
		//   - O jogador tem que ter colocado todas as cartas nos montes (sem cartas na mão)
		//   - Todos os montes do jogador devem ter cartas
		
		
		private void Awake()
		{
			TurnController.OnTurnChanged += OnTurnChanged;
		}
		
		private void OnEnable()
		{
			
		}

		private void OnDisable()
		{
			
		}

		private void OnDestroy()
		{
			TurnController.OnTurnChanged -= OnTurnChanged;
			PlayerHand player1Hand = PlayerHand.GetPlayerHand(Player.Index.Player1);
			if (player1Hand != null)
			{
				player1Hand.OnCardsUpdated -= OnPlayerHandCardsUpdated;
			}
			PlayerHand player2Hand = PlayerHand.GetPlayerHand(Player.Index.Player2);
			if (player2Hand != null)
			{
				player2Hand.OnCardsUpdated -= OnPlayerHandCardsUpdated;	
			}
		}

		private void OnTurnChanged(Player.Index turn)
		{
			_button.interactable = false;
			PlayerHand.GetPlayerHand(Player.Index.Player1).OnCardsUpdated -= OnPlayerHandCardsUpdated;
			PlayerHand.GetPlayerHand(Player.Index.Player2).OnCardsUpdated -= OnPlayerHandCardsUpdated;

			switch (GameController.Instance.Stage)
			{
				case GameController.GameStage.PlayersCreatePiles:
					PlayerHand.GetPlayerHand(turn).OnCardsUpdated += OnPlayerHandCardsUpdated;
					break;
			}
		}

		private void OnPlayerHandCardsUpdated(int cardsCount)
		{
			if ((cardsCount == 0) && AreAllPlayerCardPilesNotEmpty())
			{
				_button.interactable = true;
			}
			else
			{
				_button.interactable = false;
			}
		}

		private bool AreAllPlayerCardPilesNotEmpty()
		{
			List<CardPileFace> cardPiles =
				CardPileFace.GetCardPilesFromPlayer(TurnController.Instance.CurrentPlayerIndex);
			for (int i = 0; i < cardPiles.Count; i++)
			{
				if (cardPiles[i].Count == 0)
				{
					return false;
				}
			}
			return true;
		}
	}
}