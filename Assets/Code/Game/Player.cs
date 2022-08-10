using Code.Cards;

namespace Code.Game
{
	public sealed class Player
	{
		public enum Index
		{
			Player1 = 1,
			Player2
		}
		
		private CardSet _cards = new CardSet();
		
		public CardSet Cards
		{
			get { return _cards; }
		}
	}
}