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
	}
}