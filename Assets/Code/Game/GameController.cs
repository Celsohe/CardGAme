using System;
using System.Collections.Generic;
using Code.Cards;
using Code.Turn;
using Code.UI;
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
		
		// Evento usado para avisar quando o jogo troca de estado //
		public delegate void GameStateChanged(GameStage gameStage);
		public static event GameStateChanged OnGameStateChanged;

		private static GameController _instance;
		
		private GameStage _gameStage;
		
		public static GameController Instance
		{
			get
			{
				return _instance;
			}
		}

		public GameStage Stage
		{
			get
			{
				return _gameStage;
			}
		}

		private void Awake()
		{
			if (_instance != null)
			{
				Destroy(gameObject);
				return;
			}
			_instance = this;
		}

		private void OnEnable()
		{
			CardGiver.OnFinshedGivingCards += OnFinishedGivingCards;
			TurnController.OnTurnChanged += OnTurnChanged;
		}

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

		private void OnDisable()
		{
			CardGiver.OnFinshedGivingCards -= OnFinishedGivingCards;
			TurnController.OnTurnChanged -= OnTurnChanged;
		}

		private void OnDestroy()
		{
			if (_instance == this)
			{
				_instance = null;
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

		private void OnFinishedGivingCards()
		{
			SetStage(GameStage.PlayersCreatePiles);
		}

		private void OnTurnChanged(Player.Index turn)
		{
			switch (_gameStage)
			{
				case GameStage.PlayersCreatePiles:
					if (PlayerHand.GetPlayerHand(Player.Index.Player1).CardsCount == 0 &&
						PlayerHand.GetPlayerHand(Player.Index.Player2).CardsCount == 0)
					{
						SetStage(GameStage.CombatRounds);
					}
					break;
				case GameStage.CombatRounds:
					List<CardPileFace> player1Piles = CardPileFace.GetCardPilesFromPlayer(Player.Index.Player1);
					bool gameOver = true;
					for (int i = 0; i < player1Piles.Count; i++)
					{
						if (player1Piles[i].PileState == CardPileFace.State.Ready)
						{
							gameOver = false;
							break;
						}
					}
					if (gameOver)
					{
						SetStage(GameStage.GameOver);
					}
					break;
			}
		}
	}
}