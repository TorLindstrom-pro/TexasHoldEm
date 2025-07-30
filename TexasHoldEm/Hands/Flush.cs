using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class Flush : Hand
{
	public override bool Matching(List<Card> cards) =>
		cards
			.GroupBy(card => card.Suit)
			.Any(group => group.Count() >= 5);

	public override (string type, string[] ranks) GetHand(List<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Suit)
			.Single(group => group.Count() >= 5)
			.Take(5)
			.Select(card => card.Value)
			.ToArray();

		return ("flush", hand);
	}
}