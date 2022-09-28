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
		private string _textFormat = "Jogador {0} venceu esse combate"; 
		[SerializeField]
		private TMP_Text _textField;
		
		private void Awake()
		{
			_content.SetActive(false);
		}
		
		private void OnEnable()
		{
			CardPileFace.OnCardPileCombat += OnCardPileCombat;
			TurnController.OnTurnChanged += OnTurnChanged;
		}

		private void OnDisable()
		{
			CardPileFace.OnCardPileCombat -= OnCardPileCombat;
			TurnController.OnTurnChanged -= OnTurnChanged;
		}

		private void OnCardPileCombat(Player.Index winner)
		{
			_content.SetActive(true);

			_textField.text = string.Format(_textFormat, (int)winner);
		}

		private void OnTurnChanged(Player.Index player)
		{
			_content.SetActive(false);
		}
	}
}