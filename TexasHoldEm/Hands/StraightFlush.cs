using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class StraightFlush : Hand
{
	public override bool Matching(List<Card> cards) =>
		cards
			.GroupBy(card => card.Suit)
			.Select(group => group.ToList())
			.Any(group => group
				.Any(card => AreNextCardsInSequence(group.ToList(), card)));

	public override (string type, string[] ranks) GetHand(List<Card> cards)
	{
		var suit = cards
			.GroupBy(card => card.Suit)
			.Select(group => group.ToList())
			.First(group => group
				.Any(card => AreNextCardsInSequence(group, card)));

		var startOfStraight = suit
			.FindIndex(card => AreNextCardsInSequence(suit, card));

		var hand = suit
			.GetRange(startOfStraight, 5)
			.Select(card => card.Value)
			.ToArray();

		return ("straight-flush", hand);
	}

	private static bool AreNextCardsInSequence(List<Card> distinct, Card card)
	{
		var indexOf = distinct.IndexOf(card) + 4;

		if (indexOf < distinct.Count)
			return distinct[indexOf].Order() == card.Order() - 4;

		return false;
	}
}