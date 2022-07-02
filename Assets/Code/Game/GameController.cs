using System;
using Code.Cards;
using Code.Turn;
using UnityEngine;

namespace Code.Game
{
	public sealed class GameController : MonoBehaviour
	{
		public enum GameStage
		{
			DistributeCards,
			PlayersCreatePiles,
			CombatRounds,
			GameOver
		}
		
		public delegate void GameStateChanged(GameStage gameStage);
		public static event GameStateChanged OnGameStateChanged;
		
		private GameStage _gameStage;

		private void Start()
		{
			SetStage(GameStage.DistributeCards);
		}

		private void Update()
		{	
			if (Input.GetKeyDown(KeyCode.Space))
			{
				TurnController.Instance.ChangeTurn();
			}
		}

		private void SetStage(GameStage stage)
		{
			_gameStage = stage;
			if(OnGameStateChanged != null)
			{
				OnGameStateChanged(_gameStage);
			}
		}
	}
}