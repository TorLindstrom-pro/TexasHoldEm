using TexasHoldEm;

namespace Test;

public class KataTests
{
    [Theory(DisplayName = "Pick Highest Card if there is no hand")]
    [InlineData(new[] {"8♠", "6♦"}, new[] {"J♣", "5♥", "10♥", "2♥", "3♦"}, new [] { "J", "10", "8", "6", "5" })]
    [InlineData(new[] {"Q♠", "A♦"}, new[] {"J♣", "K♥", "10♥", "2♥", "3♦"}, new [] { "A", "K", "Q", "J", "10" })]
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
        Assert.Equal(new[] {"2", "J", "10", "6"}, ranks);
    }
}
