namespace TexasHoldEm;

public class TwoPair : Hand
{
	public override bool Matching(IEnumerable<Card> cards) => cards
		.Select(card => card.Value)
		.GroupBy(card => card)
		.Count(group => group.Count() == 2) == 2;

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards) => (
		"two pair",
		cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.ThenByDescending(card => card.First().Order())
			.Take(3)
			.Select(card => card.Key)
			.ToArray());
}