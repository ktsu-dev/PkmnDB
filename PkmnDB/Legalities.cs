// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using ktsu.Semantics;

/// <summary>
/// Represents a legality status for cards or sets in the different play formats.
/// </summary>
public sealed record Legality : SemanticString<Legality> { }

/// <summary>
/// Contains legality information for a card or set across different play formats.
/// </summary>
public class Legalities
{
	/// <summary>
	/// Gets or sets the legality status in the Unlimited format.
	/// </summary>
	public Legality Unlimited { get; set; } = new();

	/// <summary>
	/// Gets or sets the legality status in the Standard format.
	/// </summary>
	public Legality Standard { get; set; } = new();

	/// <summary>
	/// Gets or sets the legality status in the Expanded format.
	/// </summary>
	public Legality Expanded { get; set; } = new();
}
