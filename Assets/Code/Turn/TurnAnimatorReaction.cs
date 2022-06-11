using System;
using UnityEngine;

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
		
		private void OnTurnChanged(TurnController.PlayerTurn turn)
		{
			_animator.SetInteger(_turnParameterName, (int)turn);
		}
	}
}