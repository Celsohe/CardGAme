using System;
using UnityEngine;
using Code.Game;

namespace Code.Turn
{
	public sealed class TableTurner : MonoBehaviour
	{
		public delegate void TableTurned();
		public static event TableTurned OnTableTurned;
		
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

		public void TableFinishedTurning()
		{
			if (OnTableTurned != null)
			{
				OnTableTurned.Invoke();
			}
		}
		
		private void OnTurnChanged(Player.Index turn)
		{
			_animator.SetInteger(_turnParameterName, (int)turn);
		}
	}
}