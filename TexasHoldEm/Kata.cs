using System.Collections.Generic;
using System.Linq;

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
			.ToArray();

		return Hands
			.First(hand => hand.Matching(cards))
			.GetHand(cards);
	}
}

public abstract class Hand
{
	public abstract bool Matching(IEnumerable<Card> cards);
	public abstract (string type, string[] ranks) GetHand(IEnumerable<Card> cards);
}

public class Card(string card)
{
	public string Value { get; } = card[..^1];
	public char Suit { get; } = card[^1];
	
	public int Order() =>
		Value switch
		{
			"A" => 14,
			"K" => 13,
			"Q" => 12,
			"J" => 11,
			_ => int.Parse(Value)
		};
}

public class StraightFlush : Hand
{
	public override bool Matching(IEnumerable<Card> cards) =>
		cards
			.GroupBy(card => card.Suit)
			.Select(group => group
				.OrderByDescending(card => card.Order())
				.ToList())
			.Any(group => group
				.Any(card => AreNextCardsInSequence(group.ToList(), card)));

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var suit = cards
			.GroupBy(card => card.Suit)
			.Select(group => group
				.OrderByDescending(card => card.Order())
				.ToList())
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

public class FourOfAKind : Hand
{
	public override bool Matching(IEnumerable<Card> cards) =>
		cards
			.GroupBy(card => card.Value)
			.Any(group => group.Count() == 4);

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Value)
			.ToList()
			.OrderByDescending(group => group.Count() == 4)
			.ThenByDescending(group => group.First().Order())
			.SelectMany(group => group)
			.Select(card => card.Value)
			.Distinct()
			.Take(2)
			.ToArray();

		return ("four-of-a-kind", hand);
	}
}

public class FullHouse : Hand
{
	public override bool Matching(IEnumerable<Card> cards)
	{
		var groupsByValue = cards
			.GroupBy(card => card.Value)
			.ToList();

		var twoOrMore = groupsByValue.Count(group => group.Count() >= 2);
		var threeOrMore = groupsByValue.Count(group => group.Count() == 3);

		return twoOrMore >= 2 && threeOrMore >= 1;
	}

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Value)
			.ToList()
			.OrderByDescending(group => group.Count())
			.ThenByDescending(group => group.First().Order())
			.SelectMany(group => group)
			.Select(card => card.Value)
			.Distinct()
			.Take(2)
			.ToArray();

		return ("full house", hand);
	}
}

public class Flush : Hand
{
	public override bool Matching(IEnumerable<Card> cards) =>
		cards
			.GroupBy(card => card.Suit)
			.Any(group => group.Count() >= 5);

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var hand = cards
			.GroupBy(card => card.Suit)
			.Single(group => group.Count() >= 5)
			.OrderByDescending(card => card.Order())
			.Take(5)
			.Select(card => card.Value)
			.ToArray();

		return ("flush", hand);
	}
}

public class Straight : Hand
{
	public override bool Matching(IEnumerable<Card> cards)
	{
		var distinct = cards
			.OrderByDescending(card => card.Order())
			.DistinctBy(card => card.Order())
			.ToList();

		return distinct.Any(card => AreNextCardsInSequence(distinct, card));
	}

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var distinct = cards
			.OrderByDescending(card => card.Order())
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

public class ThreeOfAKind : Hand
{
	public override bool Matching(IEnumerable<Card> cards) => cards
		.Select(card => card.Value)
		.GroupBy(card => card)
		.Any(group => group.Count() == 3);

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards) => (
		"three-of-a-kind",
		cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.ThenByDescending(card => card.First().Order())
			.Take(3)
			.Select(card => card.Key)
			.ToArray());
}

public class TwoPair : Hand
{
	public override bool Matching(IEnumerable<Card> cards) => cards
		.Select(card => card.Value)
		.GroupBy(card => card)
		.Count(group => group.Count() == 2) >= 2;

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards)
	{
		var pairs = cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.ThenByDescending(card => card.First().Order())
			.Take(2)
			.Select(card => card.First())
			.Distinct();

		var single = cards
			.OrderByDescending(card => card.Order())
			.Where(card => !pairs
				.Select(card1 => card1.Order())
				.Contains(card.Order()))
			.Take(1);
		
		var hand = pairs
			.Concat(single)
			.Select(card => card.Value)
			.ToArray();
		
		return (
			"two pair",
			hand);
	}
}

public class Pairs : Hand
{
	public override bool Matching(IEnumerable<Card> cards) => cards
		.Select(card => card.Value)
		.GroupBy(card => card)
		.Any(group => group.Count() == 2);

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards) => (
		"pair",
		cards
			.GroupBy(card => card.Value)
			.OrderByDescending(card => card.Count())
			.ThenByDescending(card => card.First().Order())
			.Take(4)
			.Select(card => card.Key)
			.ToArray());
}

public class Nothing : Hand
{
	public override bool Matching(IEnumerable<Card> cards) => true;

	public override (string type, string[] ranks) GetHand(IEnumerable<Card> cards) => (
		"nothing",
		cards
			.OrderByDescending(card => card.Order())
			.Take(5)
			.Select(card => card.Value)
			.ToArray());
}