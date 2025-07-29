namespace TexasHoldEm;

public class FourOfAKind : Hand
{
	public override bool Matching(IEnumerable<Card> cards) =>
		cards
			.GroupBy(card => card.Value)
			.Any(group => group.Count() == 4);

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Value)
			.ToList()
			.OrderByDescending(group => group.Count() == 4)
			.ThenByDescending(group => group.First().Order())
			.SelectMany(group => group)
			.Select(card => card.Value)
			.Take(5)
			.ToArray();

		return ("four-of-a-kind", hand);
	}
}