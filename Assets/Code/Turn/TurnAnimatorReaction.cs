using System;
using UnityEngine;
using Code.Game;

namespace Code.Turn
{
	public sealed class TurnAnimatorReaction : MonoBehaviour
	{
		[SerializeField]
		private Animator _animator;
		[SerializeField]
		private string _turnParameterName;

		private void OnEnable()
		{
			TurnController.OnTurnChanged += OnTurnChanged;
		}
		
		private void OnDisable()
		{
			TurnController.OnTurnChanged -= OnTurnChanged;
		}
		
		private void OnTurnChanged(Player.Index turn)
		{
			_animator.SetInteger(_turnParameterName, (int)turn);
		}
	}
}