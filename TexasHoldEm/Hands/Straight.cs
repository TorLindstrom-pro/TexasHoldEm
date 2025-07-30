using System.Collections.Generic;
using System.Linq;

namespace TexasHoldEm.Hands;

public class Straight : Hand
{
	public override bool Matching(List<Card> cards)
	{
		var distinct = cards
			.DistinctBy(card => card.Order())
			.ToList();

		return distinct.Any(card => AreNextCardsInSequence(distinct, card));
	}

	public override (string type, string[] ranks) GetHand(List<Card> cards)
	{
		var distinct = cards
			.DistinctBy(card => card.Order())
			.ToList();

		var start = distinct
			.FindIndex(card => AreNextCardsInSequence(distinct, card));

		var hand = distinct
			.GetRange(start, 5)
			.Select(card => card.Value)
			.ToArray();

		return ("straight", hand);
	}

	private static bool AreNextCardsInSequence(List<Card> distinct, Card card)
	{
		var indexOf = distinct.IndexOf(card) + 4;

		if (indexOf < distinct.Count)
			return distinct[indexOf].Order() == card.Order() - 4;

		return false;
	}
}