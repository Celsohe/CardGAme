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

	[SerializeField]
	private Animator _tableAnimator;
	[SerializeField]
	private string _playerTurnParameter = "Player";
	
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
		_tableAnimator.SetInteger(_playerTurnParameter, (int)_currentTurn);
	}
}
