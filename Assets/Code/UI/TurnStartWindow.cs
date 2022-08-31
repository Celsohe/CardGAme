using System;
using Code.Game;
using Code.Turn;
using UnityEngine;

namespace Code.UI
{
	public sealed class TurnStartWindow : MonoBehaviour
	{
		[SerializeField]
		private GameObject _content;
		
		private void Awake()
		{
			TurnController.OnTurnChanged += OnTurnChanged;
			GameController.OnGameStateChanged += OnGameStateChanged;
		}

		private void OnDestroy()
		{
			TurnController.OnTurnChanged -= OnTurnChanged;
			GameController.OnGameStateChanged -= OnGameStateChanged;
		}

		private void OnTurnChanged(Player.Index turn)
		{
			if (GameController.Instance.Stage != GameController.GameStage.GameOver)
			{
				_content.SetActive(true);
			}
		}
		
		private void OnGameStateChanged(GameController.GameStage stage)
		{
			if (stage == GameController.GameStage.GameOver)
			{
				_content.SetActive(false);
			}
		}
	}
}