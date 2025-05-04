// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using ktsu.Semantics;

public sealed record ResistanceValue : SemanticString<ResistanceValue> { }

public class Resistance
{
	public CardType Type { get; set; } = new();
	public ResistanceValue Value { get; set; } = new();
}
