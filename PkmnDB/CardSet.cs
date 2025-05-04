// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using System.Text.Json.Serialization;
using ktsu.Semantics;

public sealed record CardSetId : SemanticString<CardSetId> { }
public sealed record CardSetName : SemanticString<CardSetName> { }
public sealed record CardSetCode : SemanticString<CardSetCode> { }
public sealed record SeriesName : SemanticString<SeriesName> { }
public sealed record CardSetSearchData : SemanticString<CardSetSearchData> { }

public class CardSet
{
	public CardSetId Id { get; set; } = new();
	public CardSetName Name { get; set; } = new();
	public SeriesName Series { get; set; } = new();
	public int PrintedTotal { get; set; }
	public int Total { get; set; }
	public Legalities Legalities { get; set; } = new();
	public CardSetCode PtcgoCode { get; set; } = new();
	public DateTime ReleaseDate { get; set; }
	public DateTime UpdatedAt { get; set; }
	public SetImages Images { get; set; } = new();

	[JsonIgnore]
	public CardSetSearchData CardSetSearchData => cardSetSearchDataCache ??= $"{Id} {PtcgoCode} {Series} {Name}".As<CardSetSearchData>();
	private CardSetSearchData? cardSetSearchDataCache;
}
