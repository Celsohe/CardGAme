using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TurnController : MonoBehaviour
{
	public enum PlayerTurn
	{
		Player1 = 1,
		Player2
	}

	public delegate void TurnChanged(PlayerTurn turn);
	public static event TurnChanged OnTurnChanged;
	
	private PlayerTurn _currentTurn = PlayerTurn.Player1;
	
	public void ChangeTurn()
	{
		if (_currentTurn == PlayerTurn.Player1)
		{
			_currentTurn = PlayerTurn.Player2;
		}
		else
		{
			_currentTurn = PlayerTurn.Player1;
		}

		Debug.Log("Turn changed to " + Enum.GetName(typeof(PlayerTurn), _currentTurn));
		if(OnTurnChanged != null)
		{
			OnTurnChanged(_currentTurn);
		}
	}
}
