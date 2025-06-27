namespace TexasHoldEm;

public static class Kata
{
	private static readonly Hand Pairs = new(cards => cards
			.Select(card => card.Value)
			.GroupBy(card => card)
			.Any(group => group.Count() == 2),
		cards => ("pair", cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.ThenByDescending(card => card.First().Order())
			.Take(4)
			.Select(card => card.Key)
			.ToArray()));

	private static readonly Hand Nothing = new(_ => true,
		cards => (
			"nothing",
			cards
				.OrderByDescending(card => card.Order())
				.Take(5)
				.Select(card => card.Value)
				.ToArray()));

	public static (string type, string[] ranks) Hand(string[] holeCards, string[] communityCards)
	{
		var orderedCards = holeCards
			.Concat(communityCards)
			.Select(card => new Card(card))
			.ToArray();

		return Hands
			.First(hand => hand.Matching(orderedCards))
			.GetHand(orderedCards);
	}

	public static Hand[] Hands { get; set; } =
	{
		Pairs,
		Nothing
	};
}

public class Hand(
	Func<IEnumerable<Card>, bool> matching,
	Func<IEnumerable<Card>, (string type, string[] ranks)> getHand)
{
	public Func<IEnumerable<Card>, bool> Matching { get; } = matching;
	public Func<IEnumerable<Card>, (string type, string[] ranks)> GetHand { get; } = getHand;
}