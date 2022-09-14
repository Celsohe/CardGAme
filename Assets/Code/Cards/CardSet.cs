using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Cards
{
	/// <summary>
	/// Class that set the players deck.
	/// </summary>
	public sealed class CardSet
	{
	/// <summary>
	/// Creates a private list _cards.
	/// </summary>
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

	/// <summary>
	/// Method that creates a full deck.
	/// </summary>
	public void FillDeck()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 1; j <= 13; j++)
			{
				this.AddCard(new Card((CardSuit) i, (CardValue) j));
			}
		}
	}

	/// <summary>
	/// Method that access deck, randomly access a card then delete it. Returns a Card.
	/// </summary>
	/// <returns></returns>
	public Card GetRandom()
	{
		int randomIndex = Random.Range(0, this._cards.Count);
		Card card = _cards[randomIndex];
		this.RemoveCard(card);
		return card;
	}
	}
}