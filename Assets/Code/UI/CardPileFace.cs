using System;
using System.Collections.Generic;
using Code.Cards;
using Code.Game;
using Code.Turn;
using Code.UI.Selection;
using UnityEngine;

namespace Code.UI
{
	public sealed class CardPileFace : MonoBehaviour, ISelectable, IInteractable
	{
		private static List<CardPileFace> _all = new List<CardPileFace>();
		
		[Header("Configuration")]
		[SerializeField]
		private Player.Index _playerIndex;
		[SerializeField]
		private CardFace _cardFacePrefab;
		[SerializeField]
		private CardVisualSet _cardVisualSet;
		[Header("Components")]
		[SerializeField]
		private Transform _cardsParent;
		[SerializeField]
		private GameObject _cover;
		[Header("Presentation")]
		[SerializeField]
		private float _cardDistance = .2f;
		
		private CardPile _cardPile = new CardPile();
		private List<CardFace> _cardFaces = new List<CardFace>();

		public static List<CardPileFace> GetCardPilesFromPlayer(Player.Index playerIndex)
		{
			List<CardPileFace> playerCardPiles = new List<CardPileFace>();
			for (int i = 0; i < _all.Count; i++)
			{
				if (_all[i]._playerIndex == playerIndex)
				{
					playerCardPiles.Add(_all[i]);
				}
			}
			return playerCardPiles;
		}

		public int Count
		{
			get
			{
				return _cardPile.Count;
			}
		}
		
		private void OnEnable()
		{
			TurnController.OnTurnChanged += OnTurnChanged;
			_all.Add(this);
		}

		private void Start()
		{
			OnTurnChanged(TurnController.Instance.CurrentPlayerIndex);
		}

		private void OnDisable()
		{
			TurnController.OnTurnChanged -= OnTurnChanged;
			_all.Remove(this);
		}

		public void Select()
		{
			// TODO
		}

		public void Unselect()
		{
			// TODO
		}

		public bool Interact(ISelectable selectedObject)
		{
			if (TurnController.Instance.CurrentPlayerIndex != _playerIndex)
			{
				return false;
			}
			
			if (selectedObject == null)
			{
				Card topCard = RemoveCardFromPile();
				if(topCard != null)
				{
					PlayerHand playerHand = PlayerHand.GetPlayerHand(_playerIndex);
					if (playerHand != null)
					{
						playerHand.AddCard(topCard);
						return true;
					}
				}
			}
			else if (selectedObject is SelectableCard)
			{
				SelectableCard selectedCard = (SelectableCard) selectedObject;
				AddCardToPile(selectedCard.GetComponent<CardFace>().Card);
				selectedCard.RemoveSelfFromHand();
				return true;
			}
			return false;
		}
		
		public void AddCardToPile(Card card)
		{
			_cardPile.Add(card);
			
			CardFace cardFace = Instantiate(_cardFacePrefab, _cardsParent);
			cardFace.SetFace(card, _cardVisualSet);
			cardFace.OrderInLayer = _cardFaces.Count;
			_cardFaces.Add(cardFace);

			Refresh();
		}
		
		public Card RemoveCardFromPile()
		{
			Card topCard = _cardPile.RemoveTopCard();
			if (topCard != null)
			{
				CardFace cardFace = _cardFaces[_cardFaces.Count - 1];
				_cardFaces.RemoveAt(_cardFaces.Count - 1);
				Destroy(cardFace.gameObject);
				Refresh();
			}
			return topCard;
		}

		private void Refresh()
		{
			for (int i = 0; i < _cardFaces.Count; i++)
			{
				Vector3 cardPosition = _cardFaces[i].transform.localPosition;
				cardPosition.y = -_cardDistance * i;
				_cardFaces[i].transform.localPosition = cardPosition;
				_cardFaces[i].OrderInLayer = i;
			}
		}

		private void OnTurnChanged(Player.Index turn)
		{
			if (turn == _playerIndex)
			{
				_cardsParent.gameObject.SetActive(true);
				_cover.SetActive(false);
			}
			else
			{
				_cardsParent.gameObject.SetActive(false);
				_cover.SetActive(true);
			}
		}
	}
}