using TexasHoldEm;

namespace Test;

public class KataTests
{
	[Theory(DisplayName = "Pick Highest Card if there is no hand")]
	[InlineData(new[] { "8♠", "6♦" }, new[] { "J♣", "5♥", "10♥", "2♥", "3♦" }, new[] { "J", "10", "8", "6", "5" })]
	[InlineData(new[] { "Q♠", "5♦" }, new[] { "J♣", "K♥", "10♥", "2♥", "3♦" }, new[] { "K", "Q", "J", "10", "5" })]
	public void PickHighestCard_WhenNoHand(string[] holeCards, string[] communityCards, string[] expected)
	{
		// Act
		var (type, ranks) = Kata.Hand(holeCards, communityCards);

		// Assert
		Assert.Equal("nothing", type);
		Assert.Equal(expected, ranks);
	}

	[Fact]
	public void Pairs()
	{
		// Act
		var (type, ranks) = Kata.Hand(["2♠", "6♦"], ["J♣", "5♥", "10♥", "2♥", "3♦"]);

		// Assert
		Assert.Equal("pair", type);
		Assert.Equal(new[] { "2", "J", "10", "6" }, ranks);
	}

	[Fact]
	public void TwoPairs()
	{
		// Act
		var (type, ranks) = Kata.Hand(["2♠", "6♦"], ["J♣", "5♥", "10♥", "2♥", "6♦"]);

		// Assert
		Assert.Equal("two pair", type);
		Assert.Equal(new[] { "6", "2", "J" }, ranks);
	}

	[Fact]
	public void ThreeOfAKind()
	{
		// Act
		var (type, ranks) = Kata.Hand(["2♠", "J♦"], ["2♣", "5♥", "10♥", "2♥", "6♦"]);

		// Assert
		Assert.Equal("three-of-a-kind", type);
		Assert.Equal(new[] { "2", "J", "10" }, ranks);
	}

	[Fact]
	public void Straight()
	{
		// Act
		var (type, ranks) = Kata.Hand(["2♠", "4♦"], ["3♣", "5♥", "10♥", "2♥", "6♦"]);

		// Assert
		Assert.Equal("straight", type);
		Assert.Equal(new[] { "6", "5", "4", "3", "2" }, ranks);
	}

	[Fact]
	public void Flush()
	{
		// Act
		var (type, ranks) = Kata.Hand(["2♥", "3♥"], ["3♣", "5♥", "10♥", "2♥", "6♦"]);

		// Assert
		Assert.Equal("flush", type);
		Assert.Equal(new[] { "10", "5", "3", "2", "2" }, ranks);
	}

	[Fact]
	public void FullHouse()
	{
		// Act
		var (type, ranks) = Kata.Hand(["2♥", "3♦"], ["3♣", "5♥", "10♥", "2♥", "2♦"]);

		// Assert
		Assert.Equal("full house", type);
		Assert.Equal(new[] { "2", "3" }, ranks);
	}

	[Fact]
	public void FourOfAKind()
	{
		// Act
		var (type, ranks) = Kata.Hand(["2♥", "2♦"], ["3♣", "5♥", "10♥", "2♣", "2♠"]);

		// Assert
		Assert.Equal("four-of-a-kind", type);
		Assert.Equal(new[] { "2", "10" }, ranks);
	}

	[Fact]
	public void StraightFlush()
	{
		// Act
		var (type, ranks) = Kata.Hand(["Q♥", "10♥"], ["J♥", "9♥", "8♥", "2♣", "2♠"]);

		// Assert
		Assert.Equal("straight-flush", type);
		Assert.Equal(new[] { "Q", "J", "10", "9", "8" }, ranks);
	}
}