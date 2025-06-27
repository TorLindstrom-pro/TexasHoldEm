namespace TexasHoldEm;

public abstract class Hand
{
	public abstract bool Matching(IEnumerable<Card> cards);
	public abstract (string type, string[] ranks) GetHand(IEnumerable<Card> cards);
}