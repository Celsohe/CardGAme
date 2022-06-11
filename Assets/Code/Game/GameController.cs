using System;
using UnityEngine;

namespace Code.Game
{
	public sealed class GameController : MonoBehaviour
	{
		[SerializeField]
		private TurnController _turnController;
		
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_turnController.ChangeTurn();
			}
		}
	}
}