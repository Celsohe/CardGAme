using System;
using System.Collections.Generic;
using BitStrap;
using Code.Cards;
using Code.Game;
using Code.Turn;
using UnityEngine;

namespace Code.UI
{
	public sealed class PlayerHand : MonoBehaviour
	{
		// Evento para avisar que a mão ficou vazia
		public delegate void PlayHandEvent(int cardsCount);
		public event PlayHandEvent OnCardsUpdated;
		
		private static List<PlayerHand> _playerHands = new List<PlayerHand>();
		
		[SerializeField]
		private Player.Index _playerIndex;
		[SerializeField]
		private Transform _cardsParent;
		[SerializeField]
		private float _cardRotation = 5f;
		[SerializeField]
		private float _rotationPivotDistance = 5f;
		[Header("Project References")]
		[SerializeField]
		private SelectableCard _selectableCardPrefab;
		[SerializeField]
		private CardVisualSet _cardVisualSet;

		private List<SelectableCard> _cards = new List<SelectableCard>();

		public static IReadOnlyList<PlayerHand> All
		{
			get
			{
				return _playerHands.AsReadOnly();
			}
		}

		public int CardsCount
		{
			get
			{
				return _cards.Count;
			}
		}

		public static PlayerHand GetPlayerHand(Player.Index playerIndex)
		{
			// Abaixo uma maneira de fazer a busca por um elemento da lista usando "predicado", ao invés de usar um loop:
			//return _playerHands.Find(x => x._playerIndex == playerIndex);
			for(int i = 0; i < _playerHands.Count; i++)
			{
				if(_playerHands[i]._playerIndex == playerIndex)
				{
					return _playerHands[i];
				}
			}
			return null;
		}
		
		private void OnEnable()
		{
			_playerHands.Add(this);
			TableTurner.OnTableTurned += OnTableTurned;
		}

		private void Start()
		{
			OnTableTurned();
		}

		private void OnDisable()
		{
			_playerHands.Remove(this);
			TableTurner.OnTableTurned -= OnTableTurned;
		}
		
		public void AddCard(Card card)
		{
			GameObject cardPivot = new GameObject("CardPivot");
			cardPivot.transform.SetParent(_cardsParent, false);
			SelectableCard selectableCard = Instantiate(_selectableCardPrefab, cardPivot.transform);
			selectableCard.GetComponent<CardFace>().SetFace(card, _cardVisualSet);
			selectableCard.SetHolder(this);
				
			_cards.Add(selectableCard);
			Reorder();

			if (OnCardsUpdated != null)
			{
				OnCardsUpdated.Invoke(_cards.Count);
			}
		}
		
		public bool RemoveCard(SelectableCard card)
		{
			if (_cards.Remove(card))
			{
				Destroy(card.transform.parent.gameObject);
				Reorder();
				
				if (OnCardsUpdated != null)
				{
					OnCardsUpdated.Invoke(_cards.Count);
				}
				
				return true;
			}
			return false;
		}

		[Button]
		private void Reorder()
		{
			int totalCards = _cards.Count;

			float firstCardAngle = _cardRotation * totalCards / 2;
			
			for (int i = 0; i < totalCards; i++)
			{
				SelectableCard selectableCard = _cards[i];

				Transform pivot = selectableCard.transform.parent;
				
				pivot.localPosition = new Vector3(0, -_rotationPivotDistance, 0);
				selectableCard.transform.localPosition = new Vector3(0, _rotationPivotDistance, 0);

				Quaternion rotation = Quaternion.Euler(0, 0, firstCardAngle - (i * _cardRotation));
				
				pivot.localRotation = rotation;

				selectableCard.GetComponent<CardFace>().OrderInLayer = i;
			}
		}

		private void OnTableTurned()
		{
			_cardsParent.gameObject.SetActive(TurnController.Instance.CurrentPlayerIndex == _playerIndex);
		}
	}
}