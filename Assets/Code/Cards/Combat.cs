namespace Code.Cards
{
	public sealed class Combat
	{
		public static CardPile Fight(CardPile pileA, CardPile pileB)
		{
			Combatant combatantA = new Combatant();
			combatantA.pile = pileA;
			Combatant combatantB = new Combatant();
			combatantB.pile = pileB;

			if (pileA.HasQueen)
			{
				combatantA.pile = pileB;
				combatantB.pile = pileA;
			}

			if (pileB.HasQueen)
			{
				CardPile auxPile = combatantA.pile;
				combatantA.pile = combatantB.pile;
				combatantB.pile = auxPile;
			}
			
			if(combatantA.pile.HasJack && combatantB.pile.HasKing)
			{
				return pileA;
			}
			if(combatantB.pile.HasJack && combatantA.pile.HasKing)
			{
				return pileB;
			}
			
			if(combatantA.pile.HasKing && !combatantB.pile.HasKing)
			{
				return pileA;
			}
			if(!combatantA.pile.HasKing && combatantB.pile.HasKing)
			{
				return pileB;
			}
			
			if(combatantA.pile.Value > combatantB.pile.Value)
			{
				return pileA;
			}
			else if (combatantA.pile.Value < combatantB.pile.Value)
			{
				return pileB;
			}

			return null;
		}

		private class Combatant
		{
			public CardPile pile;
		}
	}
}