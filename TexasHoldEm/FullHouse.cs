namespace TexasHoldEm;

public class FullHouse : Hand
{
	public override bool Matching(IEnumerable<Card> cards)
	{
		var groupsByValue = cards
			.GroupBy(card => card.Value)
			.ToList();

		var twoOrMore = groupsByValue.Count(group => group.Count() >= 2);
		var threeOrMore = groupsByValue.Count(group => group.Count() == 3);
		
		return twoOrMore >= 2 && threeOrMore >= 1;
	}

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Value)
			.ToList()
			.OrderByDescending(group => group.Count())
			.ThenByDescending(group => group.First().Order())
			.SelectMany(group => group)
			.Select(card => card.Value)
			.Take(5)
			.ToArray();

		return ("full house", hand);
	}
}