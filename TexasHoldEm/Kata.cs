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

public class Card(string card)
{
	public string Value { get; set; } = card[..^1];
	public char Suit { get; set; } = card[^1];
	
	public int Order() =>
		Value switch
		{
			"A" => 14,
			"K" => 13,
			"Q" => 12,
			"J" => 11,
			_ => int.Parse(Value)
		};
}