
namespace TexasHoldEm;

public class Flush : Hand
{
	public override bool Matching(IEnumerable<Card> cards) =>
		cards
			.GroupBy(card => card.Suit)
			.Any(group => group.Count() >= 5);

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Suit)
			.Single(group => group.Count() >= 5)
			.OrderByDescending(card => card.Order())
			.Take(5)
			.Select(card => card.Value)
			.ToArray();
		
		return ("flush", hand);
	}
}