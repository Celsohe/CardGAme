using System;
using Code.Game;
using Code.Turn;
using TMPro;
using UnityEngine;

namespace Code.UI
{
	public sealed class CombatEndScreen : MonoBehaviour
	{
		[SerializeField]
		private GameObject _content;
		[SerializeField]
		private GameObject _inputBlocker;
		[SerializeField]
		private string _textFormat = "Jogador {0} venceu esse combate"; 
		[SerializeField]
		private TMP_Text _textField;
		
		private void Awake()
		{
			_content.SetActive(false);
			_inputBlocker.SetActive(false);
		}
		
		private void OnEnable()
		{
			CardPileFace.OnCardPileCombat += OnCardPileCombat;
			TurnController.OnTurnChanged += OnTurnChanged;
			TableTurner.OnTableTurned += OnTableTurned;
		}

		private void OnDisable()
		{
			CardPileFace.OnCardPileCombat -= OnCardPileCombat;
			TurnController.OnTurnChanged -= OnTurnChanged;
			TableTurner.OnTableTurned -= OnTableTurned;
		}

		private void OnCardPileCombat(Player.Index winner)
		{
			_inputBlocker.SetActive(true);
			_content.SetActive(true);

			_textField.text = string.Format(_textFormat, (int)winner);
		}

		private void OnTurnChanged(Player.Index player)
		{
			_content.SetActive(false);
		}

		private void OnTableTurned()
		{
			_inputBlocker.SetActive(false);
		}
	}
}