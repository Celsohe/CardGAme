using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Cards
{
	public sealed class CardPile
	{
		private List<Card> _cards = new List<Card>();

		public int Count
		{
			get
			{
				return _cards.Count;
			}
		}
		
		public int Value
		{
			get
			{
				int value = 0;
				foreach (Card card in _cards)
				{
					value += (int)card.Value;
				}
				return value;
			}
		}
		
		public bool HasQueen
		{
			get
			{
				foreach (Card card in _cards)
				{
					if (card.Value == CardValue.Queen)
					{
						return true;
					}
				}
				return false;
			}
		}
		
		public bool HasKing
		{
			get
			{
				foreach (Card card in _cards)
				{
					if (card.Value == CardValue.King)
					{
						return true;
					}
				}
				return false;
			}
		}
		
		public bool HasJack
		{
			get
			{
				foreach (Card card in _cards)
				{
					if (card.Value == CardValue.Jack)
					{
						return true;
					}
				}
				return false;
			}
		}

		public CardPile()
		{
			_cards = new List<Card>();
			//Debug.Log("CardPile created");
		}

		public CardPile(CardPile original)
		{
			_cards = new List<Card>(original._cards);
		}
		
		public void Add(Card card)
		{
			_cards.Add(card);
			//Debug.Log($"Card {Enum.GetName(typeof(CardValue), card.Value)} {Enum.GetName(typeof(CardSuit), card.Suit)} added to pile");
		}
		
		public void Remove(Card card)
		{
			_cards.Remove(card);
		}

		public Card RemoveTopCard()
		{
			if(_cards.Count > 0)
			{
				Card card = _cards[_cards.Count - 1];
				_cards.RemoveAt(_cards.Count - 1);
				return card;
			}
			return null;
		}
	}
}