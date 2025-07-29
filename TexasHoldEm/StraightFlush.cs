namespace TexasHoldEm;

public class StraightFlush : Hand
{
	public override bool Matching(IEnumerable<Card> cards) =>
		cards
			.GroupBy(card => card.Suit)
			.Select(group => group
				.OrderByDescending(card => card.Value)
				.ToList())
			.Any(group => group
				.Any(card => AreNextCardsInSequence(group.ToList(), card)));

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Suit)
			.Select(group => group
				.OrderByDescending(card => card.Value)
				.ToList())
			.First(group => group
				.Any(card => AreNextCardsInSequence(group, card)))
			.Select(card => card.Value)
			.Take(5)
			.ToArray();

		return ("straight flush", hand);
	}

	private static bool AreNextCardsInSequence(List<Card> distinct, Card card)
	{
		var indexOf = distinct.IndexOf(card) + 4;

		if (indexOf < distinct.Count)
			return distinct[indexOf].Order() == card.Order() - 4;

		return false;
	}
}