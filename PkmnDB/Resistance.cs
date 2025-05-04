// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using ktsu.Semantics;

/// <summary>
/// Represents a resistance value for a Pokemon card.
/// </summary>
public sealed record ResistanceValue : SemanticString<ResistanceValue> { }

/// <summary>
/// Represents a card's resistance to a specific type of attack.
/// </summary>
public class Resistance
{
	/// <summary>
	/// Gets or sets the type of attack this resistance applies to.
	/// </summary>
	public CardType Type { get; set; } = new();

	/// <summary>
	/// Gets or sets the resistance value, typically a negative number indicating damage reduction.
	/// </summary>
	public ResistanceValue Value { get; set; } = new();
}
