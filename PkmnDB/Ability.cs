// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using ktsu.Semantics;

public sealed record AbilityName : SemanticString<AbilityName> { }
public sealed record AbilityText : SemanticString<AbilityText> { }
public sealed record AbilityType : SemanticString<AbilityType> { }

public class Ability
{
	public AbilityName Name { get; set; } = new();
	public AbilityText Text { get; set; } = new();
	public AbilityType Type { get; set; } = new();
}
