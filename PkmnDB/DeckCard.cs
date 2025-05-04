// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

/// <summary>
/// Represents a card in a deck with its count.
/// </summary>
public class DeckCard
{
	/// <summary>
	/// Gets or sets the unique identifier of the card.
	/// </summary>
	public CardId Id { get; set; } = new();

	/// <summary>
	/// Gets or sets the name of the card.
	/// </summary>
	public CardName Name { get; set; } = new();

	/// <summary>
	/// Gets or sets the rarity of the card.
	/// </summary>
	public CardRarity Rarity { get; set; } = new();

	/// <summary>
	/// Gets or sets the number of copies of this card in the deck.
	/// </summary>
	public int Count { get; set; }
}
