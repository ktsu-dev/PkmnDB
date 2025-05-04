// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using System.Collections.Generic;
using ktsu.Semantics;

/// <summary>
/// Represents a deck identifier.
/// </summary>
public sealed record DeckId : SemanticString<DeckId> { }

/// <summary>
/// Represents a deck name.
/// </summary>
public sealed record DeckName : SemanticString<DeckName> { }

/// <summary>
/// Represents a deck of Pokemon cards.
/// </summary>
public class Deck
{
	private readonly List<CardType> _types = [];
	private readonly List<DeckCard> _cards = [];

	/// <summary>
	/// Gets or sets the unique identifier for the deck.
	/// </summary>
	public DeckId Id { get; set; } = new();

	/// <summary>
	/// Gets or sets the name of the deck.
	/// </summary>
	public DeckName Name { get; set; } = new();

	/// <summary>
	/// Gets the collection of card types in the deck.
	/// </summary>
	public IReadOnlyCollection<CardType> Types => _types.AsReadOnly();

	/// <summary>
	/// Gets the collection of cards in the deck.
	/// </summary>
	public IReadOnlyCollection<DeckCard> Cards => _cards.AsReadOnly();

	/// <summary>
	/// Adds a card type to the deck.
	/// </summary>
	/// <param name="type">The card type to add.</param>
	public void AddType(CardType type) => _types.Add(type);

	/// <summary>
	/// Adds a card to the deck.
	/// </summary>
	/// <param name="card">The card to add.</param>
	public void AddCard(DeckCard card) => _cards.Add(card);
}
