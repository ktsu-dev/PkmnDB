namespace ktsu.PkmnDB;

using ktsu.StrongStrings;

public sealed record ResistanceValue : StrongStringAbstract<ResistanceValue> { }

public class Resistance
{
	public CardType Type { get; set; } = new();
	public ResistanceValue Value { get; set; } = new();
}
