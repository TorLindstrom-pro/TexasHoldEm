namespace TexasHoldEm;

public static class Kata
{
	public static (string type, string[] ranks) Hand(string[] holeCards, string[] communityCards)
	{
		var orderedCards = holeCards
			.Concat(communityCards)
			.Select(card => new Card(card))
			.OrderByDescending(card => card.Order())
			.Take(5)
			.Select(card => card.Value)
			.ToArray();
		
		return ("nothing", orderedCards);
	}
}