using UnityEngine;

namespace Code.Cards
{
	/// <summary>
	/// Create a sealed class that return suits and values of cards;
	/// Sealed classes prevents other classes from inheriting from it.
	/// </summary>
	public sealed class Card
	{
		/// <summary>
		/// Creates a variable "_suit" of the type CardSuit.
		/// </summary>
		private CardSuit _suit;
		/// <summary>
		/// Creates a variable "_value" of the type CardValue.
		/// </summary>
		private CardValue _value;
		/// <summary>
		/// Method that returns the _suit "value".
		/// </summary>
		public CardSuit Suit
		{
			get { return _suit; }
		}
		/// <summary>
		/// Method that return _value variable.
		/// </summary>
		public CardValue Value
		{
			get { return _value; }
		}
		
		
		/// <summary>
		/// Method that determines values suits and valuer for a card.
		/// </summary>
		/// <param name="suit"></param>
		/// <param name="value"></param>
		public Card(CardSuit suit, CardValue value)
		{
			_suit = suit;
			_value = value;
		}
	}
}