// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using System.Collections.ObjectModel;
using ktsu.Semantics;

/// <summary>
/// Represents the name of an attack.
/// </summary>
public sealed record AttackName : SemanticString<AttackName> { }
/// <summary>
/// Represents the cost of an attack.
/// </summary>
public sealed record AttackCost : SemanticString<AttackCost> { }
/// <summary>
/// Represents the damage of an attack.
/// </summary>
public sealed record AttackDamage : SemanticString<AttackDamage> { }
/// <summary>
/// Represents the text description of an attack.
/// </summary>
public sealed record AttackText : SemanticString<AttackText> { }

/// <summary>
/// Represents an attack with its properties.
/// </summary>
public class Attack
{
	/// <summary>
	/// Gets or sets the name of the attack.
	/// </summary>
	public AttackName Name { get; set; } = new();

	/// <summary>
	/// Gets or sets the cost of the attack.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Gets set by json serializer")]
	public Collection<AttackCost> Cost { get; set; } = [];

	/// <summary>
	/// Gets or sets the converted energy cost of the attack.
	/// </summary>
	public int ConvertedEnergyCost { get; set; }

	/// <summary>
	/// Gets or sets the damage of the attack.
	/// </summary>
	public AttackDamage Damage { get; set; } = new();

	/// <summary>
	/// Gets or sets the text description of the attack.
	/// </summary>
	public AttackText Text { get; set; } = new();
}
