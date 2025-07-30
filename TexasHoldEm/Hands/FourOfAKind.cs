using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class FourOfAKind : Hand
{
	public override bool Matching(List<Card> cards) =>
		cards
			.GroupBy(card => card.Value)
			.Any(group => group.Count() == 4);

	public override (string type, string[] ranks) GetHand(List<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Value)
			.ToList()
			.OrderByDescending(group => group.Count() == 4)
			.SelectMany(group => group)
			.Select(card => card.Value)
			.Distinct()
			.Take(2)
			.ToArray();

		return ("four-of-a-kind", hand);
	}
}