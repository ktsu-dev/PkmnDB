// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using ktsu.Semantics;

/// <summary>
/// Represents an ability name for a Pokemon card.
/// </summary>
public sealed record AbilityName : SemanticString<AbilityName> { }

/// <summary>
/// Represents the text description of an ability.
/// </summary>
public sealed record AbilityText : SemanticString<AbilityText> { }

/// <summary>
/// Represents the type of an ability.
/// </summary>
public sealed record AbilityType : SemanticString<AbilityType> { }

/// <summary>
/// Represents an ability on a Pokemon card.
/// </summary>
public class Ability
{
	/// <summary>
	/// Gets or sets the name of the ability.
	/// </summary>
	public AbilityName Name { get; set; } = new();

	/// <summary>
	/// Gets or sets the text description of the ability.
	/// </summary>
	public AbilityText Text { get; set; } = new();

	/// <summary>
	/// Gets or sets the type of the ability.
	/// </summary>
	public AbilityType Type { get; set; } = new();
}
