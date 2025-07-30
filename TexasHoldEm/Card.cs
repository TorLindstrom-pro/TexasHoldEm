namespace TexasHoldEm;

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