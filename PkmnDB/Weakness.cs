// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using ktsu.Semantics;

public sealed record WeaknessValue : SemanticString<WeaknessValue> { }

public class Weakness
{
	public CardType Type { get; set; } = new();
	public WeaknessValue Value { get; set; } = new();
}
