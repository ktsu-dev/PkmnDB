namespace ktsu.PkmnDB;

using System.Collections.Generic;
using ktsu.StrongStrings;

public sealed record AttackName : StrongStringAbstract<AttackName> { }
public sealed record AttackCost : StrongStringAbstract<AttackCost> { }
public sealed record AttackDamage : StrongStringAbstract<AttackDamage> { }
public sealed record AttackText : StrongStringAbstract<AttackText> { }

public class Attack
{
	public AttackName Name { get; set; } = new();
	public List<AttackCost> Cost { get; set; } = [];
	public int ConvertedEnergyCost { get; set; }
	public AttackDamage Damage { get; set; } = new();
	public AttackText Text { get; set; } = new();
}
