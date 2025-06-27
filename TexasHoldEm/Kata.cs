namespace TexasHoldEm;

public static class Kata
{
	private static Hand[] Hands { get; } =
	[
		new TwoPair(),
		new Pairs(),
		new Nothing()
	];

	public static (string type, string[] ranks) Hand(string[] holeCards, string[] communityCards)
	{
		var cards = holeCards
			.Concat(communityCards)
			.Select(card => new Card(card))
			.ToArray();

		return Hands
			.First(hand => hand.Matching(cards))
			.GetHand(cards);
	}
}