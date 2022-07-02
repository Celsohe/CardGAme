using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Cards
{
	public sealed class CardSet
	{
		private List<Card> _cards = new List<Card>();

		public IReadOnlyList<Card> Cards
		{
			get
			{
				return _cards.AsReadOnly();
			}
		}

		public void AddCard(Card card)
		{
			this._cards.Add(card);
		}
		
		public void RemoveCard(Card card)
		{
			this._cards.Remove(card);
		}

		public void FillDeck()
		{
			for(int i = 0; i < 4; i++)
			{
				for(int j = 1; j <= 13; j++)
				{
					this.AddCard(new Card((CardSuit)i, (CardValue)j));
				}
			}
		}

		public Card GetRandom()
		{
			int randomIndex = Random.Range(0, this._cards.Count);
			Card card = _cards[randomIndex];
			this.RemoveCard(card);
			return card;
		}
	}
}