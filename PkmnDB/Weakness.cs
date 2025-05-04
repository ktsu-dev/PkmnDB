// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using ktsu.Semantics;

/// <summary>
/// Represents a weakness value for a Pokemon card.
/// </summary>
public sealed record WeaknessValue : SemanticString<WeaknessValue> { }

/// <summary>
/// Represents a card's weakness to a specific type of attack.
/// </summary>
public class Weakness
{
	/// <summary>
	/// Gets or sets the type of attack this weakness applies to.
	/// </summary>
	public CardType Type { get; set; } = new();

	/// <summary>
	/// Gets or sets the weakness value, typically a multiplier for damage taken.
	/// </summary>
	public WeaknessValue Value { get; set; } = new();
}
