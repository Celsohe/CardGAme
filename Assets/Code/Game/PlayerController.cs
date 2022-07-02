using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Game
{
	public sealed class PlayerController : MonoBehaviour
	{
		private static PlayerController _instance;

		[SerializeField]
		private int _totalPlayers = 2;
		
		private List<Player> _players = new List<Player>();

		public static PlayerController Instance
		{
			get
			{
				return _instance;
			}
		}
		
		public void Awake()
		{
			if (_instance != null)
			{
				Destroy(gameObject);
				return;
			}
			_instance = this;
			
			for(int i = 0; i < _totalPlayers; i++)
			{
				_players.Add(new Player());
			}
		}

		public void OnDestroy()
		{
			if (_instance == this)
			{
				_instance = null;
			}
		}

		public Player GetPlayer(Player.Index playerIndex)
		{
			return _players[(int)playerIndex - 1];
		}
	}
}