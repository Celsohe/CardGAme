using System;
using Code.Cards;
using Code.Turn;
using UnityEngine;

namespace Code.Game
{
	public sealed class GameController : MonoBehaviour
	{
		[SerializeField]
		private TurnController _turnController;

		private void Start()
		{
			CardPile player1Pile = new CardPile();
			Card card = new Card(CardSuit.Clubs, CardValue.Queen);
			player1Pile.Add(card);
			card = new Card(CardSuit.Clubs, CardValue.Nine);
			player1Pile.Add(card);
			
			CardPile player2Pile = new CardPile();
			card = new Card(CardSuit.Diamonds, CardValue.King);
			player2Pile.Add(card);

			CardPile winner = Combat.Fight(player1Pile, player2Pile);
			if (winner == player1Pile)
			{
				Debug.Log("Player 1 wins");
			}
			else if(winner == player2Pile)
			{
				Debug.Log("Player 2 wins");
			}
			else
			{
				Debug.Log("Draw");
			}
		}

		private void Update()
		{	
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_turnController.ChangeTurn();
			}
		}
	}
}