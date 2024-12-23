namespace ktsu.PkmnDB;

using ktsu.StrongStrings;

public sealed record AbilityName : StrongStringAbstract<AbilityName> { }
public sealed record AbilityText : StrongStringAbstract<AbilityText> { }
public sealed record AbilityType : StrongStringAbstract<AbilityType> { }

public class Ability
{
	public AbilityName Name { get; set; } = new();
	public AbilityText Text { get; set; } = new();
	public AbilityType Type { get; set; } = new();
}
