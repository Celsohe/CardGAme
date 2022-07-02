using System.Collections.Generic;
using UnityEngine;

namespace Code.Cards
{
	[CreateAssetMenu(fileName = "NewCardVisualSet", menuName = "Cards/Visual Set")]
	public sealed class CardVisualSet : ScriptableObject
	{
		[SerializeField]
		private List<Sprite> _cardSprites = new List<Sprite>();

		public Sprite GetCardSprite(Card card)
		{
			return GetByName($"{GetValueName(card.Value)}_{GetSuitName(card.Suit)}");
		}
		
		public Sprite GetBackSprite()
		{
			return GetByName("Back");
		}

		private Sprite GetByName(string name)
		{
			for(int i = 0; i < _cardSprites.Count; i++)
			{
				if(_cardSprites[i].name == name)
				{
					return _cardSprites[i];
				}
			}
			return null;
		}

		private string GetSuitName(CardSuit suit)
		{
			switch (suit)
			{
				case CardSuit.Clubs:
					return "C";
				case CardSuit.Diamonds:
					return "D";
				case CardSuit.Hearts:
					return "H";
				case CardSuit.Swords:
					return "S";
			}
			return null;
		}

		private string GetValueName(CardValue value)
		{
			switch (value)
			{
				case CardValue.Ace:
					return "A";
				case CardValue.Jack:
					return "J";
				case CardValue.Queen:
					return "Q";
				case CardValue.King:
					return "K";
				default:
					return ((int)value).ToString();
			}
		}
	}
}