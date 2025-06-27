namespace TexasHoldEm.Hands;

public class ThreeOfAKind : Hand
{
	public override bool Matching(IEnumerable<Card> cards) => cards
		.Select(card => card.Value)
		.GroupBy(card => card)
		.Any(group => group.Count() == 3);

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards) => (
		"three-of-a-kind",
		cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.ThenByDescending(card => card.First().Order())
			.Take(3)
			.Select(card => card.Key)
			.ToArray());
}