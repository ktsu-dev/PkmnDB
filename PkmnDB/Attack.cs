namespace ktsu.PkmnDB;

using System.Collections.Generic;
using ktsu.Semantics;

public sealed record AttackName : SemanticString<AttackName> { }
public sealed record AttackCost : SemanticString<AttackCost> { }
public sealed record AttackDamage : SemanticString<AttackDamage> { }
public sealed record AttackText : SemanticString<AttackText> { }

public class Attack
{
	public AttackName Name { get; set; } = new();
	public List<AttackCost> Cost { get; set; } = [];
	public int ConvertedEnergyCost { get; set; }
	public AttackDamage Damage { get; set; } = new();
	public AttackText Text { get; set; } = new();
}
