namespace TexasHoldEm;

public class Hand(
	Func<IEnumerable<Card>, bool> matching,
	Func<IEnumerable<Card>, (string type, string[] ranks)> getHand)
{
	public Func<IEnumerable<Card>, bool> Matching { get; } = matching;
	public Func<IEnumerable<Card>, (string type, string[] ranks)> GetHand { get; } = getHand;
}