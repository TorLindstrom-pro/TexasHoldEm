namespace TexasHoldEm;

public class Pairs : Hand
{
	public override bool Matching(IEnumerable<Card> cards) => cards
		.Select(card => card.Value)
		.GroupBy(card => card)
		.Any(group => group.Count() == 2);

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards) => (
		"pair",
		cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.ThenByDescending(card => card.First().Order())
			.Take(4)
			.Select(card => card.Key)
			.ToArray());
}