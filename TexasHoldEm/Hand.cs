using System.Collections.Generic;

namespace TexasHoldEm;

public abstract class Hand
{
	public abstract bool Matching(List<Card> cards);
	public abstract (string type, string[] ranks) GetHand(List<Card> cards);
}