namespace ktsu.PkmnDB;

using ktsu.StrongStrings;

public sealed record Legality : StrongStringAbstract<Legality> { }


public class Legalities
{
	public Legality Unlimited { get; set; } = new();
	public Legality Standard { get; set; } = new();
	public Legality Expanded { get; set; } = new();
}
