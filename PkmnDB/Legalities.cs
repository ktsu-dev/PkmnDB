namespace ktsu.PkmnDB;

using ktsu.Semantics;

public sealed record Legality : SemanticString<Legality> { }


public class Legalities
{
	public Legality Unlimited { get; set; } = new();
	public Legality Standard { get; set; } = new();
	public Legality Expanded { get; set; } = new();
}
