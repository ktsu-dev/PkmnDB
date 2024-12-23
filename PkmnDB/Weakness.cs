namespace ktsu.PkmnDB;

using ktsu.StrongStrings;

public sealed record WeaknessValue : StrongStringAbstract<WeaknessValue> { }

public class Weakness
{
	public CardType Type { get; set; } = new();
	public WeaknessValue Value { get; set; } = new();
}
