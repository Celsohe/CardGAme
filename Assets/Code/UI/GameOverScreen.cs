using System;
using System.Collections.Generic;
using Code.Game;
using TMPro;
using UnityEngine;

namespace Code.UI
{
	public class GameOverScreen : MonoBehaviour
	{
		[SerializeField]
		private GameObject _content;
		[SerializeField]
		private string _textFormat = "Jogador {0}"; 
		[SerializeField]
		private TMP_Text _textField;

		private void Awake()
		{
			GameController.OnGameStateChanged += OnGameStateChange;
			_content.SetActive(false);
		}

		private void OnDestroy()
		{
			GameController.OnGameStateChanged -= OnGameStateChange;
		}

		private void OnGameStateChange(GameController.GameStage stage)
		{
			if (stage == GameController.GameStage.GameOver)
			{
				_content.SetActive(true);

				_textField.text = string.Format(_textFormat, (int)GetWinner());
			}
		}

		private Player.Index GetWinner()
		{
			int player1Victories = 0;
			List<CardPileFace> player1Piles = CardPileFace.GetCardPilesFromPlayer(Player.Index.Player1);
			for (int i = 0; i < player1Piles.Count; i++)
			{
				if (player1Piles[i].PileState == CardPileFace.State.Winner)
				{
					player1Victories += 1;
				}
			}

			if (player1Victories > player1Piles.Count / 2)
			{
				return Player.Index.Player1;
			}
			return Player.Index.Player2;
		}
	}
}