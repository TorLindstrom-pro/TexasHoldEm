using TexasHoldEm;

namespace Test;

public class KataTests
{
    [Fact(DisplayName = "Pick Highest Card if there is no hand")]
    public void PickHighestCard_WhenNoHand()
    {
        // Act
        var (type, ranks) = Kata.Hand(["8♠", "6♦"], ["J♣", "5♥", "10♥", "2♥", "3♦"]);
        
        // Assert
        Assert.Equal("nothing", type);
        Assert.Equal(new [] { "J", "10", "8", "6", "5" }, ranks);
    }
}
