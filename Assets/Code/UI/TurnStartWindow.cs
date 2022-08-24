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
		}

		private void OnDestroy()
		{
			TurnController.OnTurnChanged -= OnTurnChanged;
		}

		private void OnTurnChanged(Player.Index turn)
		{
			_content.SetActive(true);
		}
	}
}