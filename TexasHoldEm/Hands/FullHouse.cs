using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class FullHouse : Hand
{
	public override bool Matching(List<Card> cards)
	{
		var groupsByValue = cards
			.GroupBy(card => card.Value)
			.ToList();

		var twoOrMore = groupsByValue.Count(group => group.Count() >= 2);
		var threeOrMore = groupsByValue.Count(group => group.Count() == 3);

		return twoOrMore >= 2 && threeOrMore >= 1;
	}

	public override (string type, string[] ranks) GetHand(List<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Value)
			.ToList()
			.OrderByDescending(group => group.Count())
			.SelectMany(group => group)
			.Select(card => card.Value)
			.Distinct()
			.Take(2)
			.ToArray();

		return ("full house", hand);
	}
}