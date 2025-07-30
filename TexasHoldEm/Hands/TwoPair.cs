using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class TwoPair : Hand
{
	public override bool Matching(List<Card> cards) => cards
		.Select(card => card.Value)
		.GroupBy(card => card)
		.Count(group => group.Count() == 2) >= 2;

	public override (string type, string[] ranks) GetHand(List<Card> cards)
	{
		var pairs = cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.Take(2)
			.Select(card => card.First())
			.Distinct();

		var single = cards
			.Where(card => !pairs
				.Select(card1 => card1.Order())
				.Contains(card.Order()))
			.Take(1);

		var hand = pairs
			.Concat(single)
			.Select(card => card.Value)
			.ToArray();

		return ("two pair", hand);
	}
}