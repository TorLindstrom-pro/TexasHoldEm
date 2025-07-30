using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class Nothing : Hand
{
	public override bool Matching(List<Card> cards) => true;

	public override (string type, string[] ranks) GetHand(List<Card> cards) => (
		"nothing",
		cards
			.Take(5)
			.Select(card => card.Value)
			.ToArray());
}