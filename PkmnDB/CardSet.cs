// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using System.Text.Json.Serialization;
using ktsu.Semantics;

/// <summary>
/// Represents a CardSet identifier.
/// </summary>
public sealed record CardSetId : SemanticString<CardSetId> { }

/// <summary>
/// Represents a CardSet name.
/// </summary>
public sealed record CardSetName : SemanticString<CardSetName> { }

/// <summary>
/// Represents a CardSet code.
/// </summary>
public sealed record CardSetCode : SemanticString<CardSetCode> { }

/// <summary>
/// Represents a Series name.
/// </summary>
public sealed record SeriesName : SemanticString<SeriesName> { }

/// <summary>
/// Represents search data for a CardSet.
/// </summary>
public sealed record CardSetSearchData : SemanticString<CardSetSearchData> { }

/// <summary>
/// Represents a set of cards in the Pokemon Trading Card Game.
/// </summary>
public class CardSet
{
	/// <summary>
	/// Gets or sets the unique identifier for the card set.
	/// </summary>
	public CardSetId Id { get; set; } = new();

	/// <summary>
	/// Gets or sets the name of the card set.
	/// </summary>
	public CardSetName Name { get; set; } = new();

	/// <summary>
	/// Gets or sets the series name the card set belongs to.
	/// </summary>
	public SeriesName Series { get; set; } = new();

	/// <summary>
	/// Gets or sets the total number of cards printed in the set.
	/// </summary>
	public int PrintedTotal { get; set; }

	/// <summary>
	/// Gets or sets the total number of cards in the set.
	/// </summary>
	public int Total { get; set; }

	/// <summary>
	/// Gets or sets the legalities information for the card set.
	/// </summary>
	public Legalities Legalities { get; set; } = new();

	/// <summary>
	/// Gets or sets the Pokemon Trading Card Game Online code for the set.
	/// </summary>
	public CardSetCode PtcgoCode { get; set; } = new();

	/// <summary>
	/// Gets or sets the release date of the card set.
	/// </summary>
	public DateTime ReleaseDate { get; set; }

	/// <summary>
	/// Gets or sets the date when the card set information was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the images associated with the card set.
	/// </summary>
	public SetImages Images { get; set; } = new();

	/// <summary>
	/// Gets the combined search data for the card set.
	/// </summary>
	[JsonIgnore]
	public CardSetSearchData CardSetSearchData => cardSetSearchDataCache ??= $"{Id} {PtcgoCode} {Series} {Name}".As<CardSetSearchData>();
	private CardSetSearchData? cardSetSearchDataCache;
}
