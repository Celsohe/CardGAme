using System;
using System.Collections;
using System.Collections.Generic;
using Code.Game;
using UnityEngine;

namespace Code.Turn
{
	public sealed class TurnController : MonoBehaviour
	{
		public delegate void TurnChanged(Player.Index turn);
		public static event TurnChanged OnTurnChanged;
		
		private static TurnController _instance;
		
		private Player.Index _currentTurn = Player.Index.Player1;

		public static TurnController Instance
		{
			get { return _instance; }
		}
		
		public Player.Index CurrentPlayerIndex
		{
			get { return _currentTurn; }
		}

		private void Awake()
		{
			if(_instance != null)
			{
				Destroy(gameObject);
				return;
			}
			_instance = this;
		}

		private void OnDestroy()
		{
			if (_instance == this)
			{
				_instance = null;
			}
		}

		public void ChangeTurn()
		{
			if (_currentTurn == Player.Index.Player1)
			{
				_currentTurn = Player.Index.Player2;
			}
			else
			{
				_currentTurn = Player.Index.Player1;
			}

			Debug.Log("Turn changed to " + Enum.GetName(typeof(Player.Index), _currentTurn));
			if (OnTurnChanged != null)
			{
				OnTurnChanged(_currentTurn);
			}
		}
	}
}