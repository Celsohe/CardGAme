using Code.Game;
using Code.Turn;
using TMPro;
using UnityEngine;

namespace Code.UI
{
	public sealed class TurnStartWindowLabel : MonoBehaviour
	{
		[SerializeField]
		private string _textFormat = "Vez do jogador {0}"; 
		[SerializeField]
		private TMP_Text _textField;
		
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
			_textField.text = string.Format(_textFormat, (int)turn);
		}
	}
}