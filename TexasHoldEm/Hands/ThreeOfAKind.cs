using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class ThreeOfAKind : Hand
{
	public override bool Matching(List<Card> cards) => cards
		.Select(card => card.Value)
		.GroupBy(card => card)
		.Any(group => group.Count() == 3);

	public override (string type, string[] ranks) GetHand(List<Card> cards) => (
		"three-of-a-kind",
		cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.Take(3)
			.Select(card => card.Key)
			.ToArray());
}