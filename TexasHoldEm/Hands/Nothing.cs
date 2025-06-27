namespace TexasHoldEm.Hands;

public class Nothing : Hand
{
	public override bool Matching(IEnumerable<Card> cards) => true;

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards) => (
		"nothing",
		cards
			.OrderByDescending(card => card.Order())
			.Take(5)
			.Select(card => card.Value)
			.ToArray());
}