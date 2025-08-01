﻿using System.Linq;
using TexasHoldEm.Hands;

namespace TexasHoldEm;

public static class Kata
{
	private static Hand[] Hands { get; } =
	[
		new StraightFlush(),
		new FourOfAKind(),
		new FullHouse(),
		new Flush(),
		new Straight(),
		new ThreeOfAKind(),
		new TwoPair(),
		new Pairs(),
		new Nothing()
	];

	public static (string type, string[] ranks) Hand(string[] holeCards, string[] communityCards)
	{
		var cards = holeCards
			.Concat(communityCards)
			.Select(card => new Card(card))
			.OrderByDescending(card => card.Order())
			.ToList();

		return Hands
			.First(hand => hand.Matching(cards))
			.GetHand(cards);
	}
}