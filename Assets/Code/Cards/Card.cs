using UnityEngine;

namespace Code.Cards
{
	public sealed class Card
	{
		private CardSuit _suit;
		private CardValue _value;
		
		public CardSuit Suit
		{
			get { return _suit; }
		}
		
		public CardValue Value
		{
			get { return _value; }
		}
		
		public Card(CardSuit suit, CardValue value)
		{
			_suit = suit;
			_value = value;
		}
	}
}