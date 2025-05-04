// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using ktsu.Semantics;

public sealed record DeckId : SemanticString<DeckId> { }
public sealed record DeckName : SemanticString<DeckName> { }

public class Deck
{
	public DeckId Id { get; set; } = new();
	public DeckName Name { get; set; } = new();
	public List<CardType> Types { get; set; } = [];
	public List<DeckCard> Cards { get; set; } = [];
}
