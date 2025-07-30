using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class Pairs : Hand
{
	public override bool Matching(List<Card> cards) => cards
		.GroupBy(card => card.Value)
		.Any(group => group.Count() == 2);

	public override (string type, string[] ranks) GetHand(List<Card> cards) => (
		"pair",
		cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.Take(4)
			.Select(card => card.Key)
			.ToArray());
}