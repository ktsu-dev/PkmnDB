namespace ktsu.PkmnDB;

using ktsu.Semantics;

public sealed record ResistanceValue : SemanticString<ResistanceValue> { }

public class Resistance
{
	public CardType Type { get; set; } = new();
	public ResistanceValue Value { get; set; } = new();
}
