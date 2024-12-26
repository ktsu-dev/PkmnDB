namespace ktsu.PkmnDB;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using ktsu.Semantics;

public sealed record CardId : SemanticString<CardId> { }
public sealed record CardName : SemanticString<CardName> { }
public sealed record CardSupertype : SemanticString<CardSupertype> { }
public sealed record CardSubtype : SemanticString<CardSubtype> { }
public sealed record CardLevel : SemanticString<CardLevel> { }
public sealed record CardHp : SemanticString<CardHp> { }
public sealed record CardType : SemanticString<CardType> { }
public sealed record CardRetreatCost : SemanticString<CardRetreatCost> { }
public sealed record CardNumber : SemanticString<CardNumber> { }
public sealed record CardArtist : SemanticString<CardArtist> { }
public sealed record CardRarity : SemanticString<CardRarity> { }
public sealed record CardFlavorText : SemanticString<CardFlavorText> { }
public sealed record CardRegulationMark : SemanticString<CardRegulationMark> { }
public sealed record CardSearchData : SemanticString<CardSearchData> { }

public class Card
{
	public CardId Id { get; set; } = new();
	public CardName Name { get; set; } = new();
	public CardSupertype Supertype { get; set; } = new();
	public List<CardSubtype> Subtypes { get; set; } = [];
	public CardLevel Level { get; set; } = new();
	public CardHp Hp { get; set; } = new();
	public List<CardType> Types { get; set; } = [];
	public CardName EvolvesFrom { get; set; } = new();
	public List<Ability> Abilities { get; set; } = [];
	public List<Attack> Attacks { get; set; } = [];
	public List<Weakness> Weaknesses { get; set; } = [];
	public List<Resistance> Resistances { get; set; } = [];
	public List<CardRetreatCost> RetreatCost { get; set; } = [];
	public int ConvertedRetreatCost { get; set; }
	public CardNumber Number { get; set; } = new();
	public CardArtist Artist { get; set; } = new();
	public CardRarity Rarity { get; set; } = new();
	public CardFlavorText FlavorText { get; set; } = new();
	public List<int> NationalPokedexNumbers { get; set; } = [];
	public Legalities Legalities { get; set; } = new();
	public CardRegulationMark RegulationMark { get; set; } = new();
	public CardImages Images { get; set; } = new();

	internal CardSetId ParseSetId() => Id.Split('-').First().As<CardSetId>();

	[JsonIgnore]
	public CardSetId CardSetId => cardSetIdCache ??= ParseSetId();
	private CardSetId? cardSetIdCache;

	[JsonIgnore]
	public CardSet CardSet
	{
		get
		{
			if (cardSetCache is null)
			{
				PkmnDB.CardSets.TryGetValue(CardSetId, out cardSetCache);
			}

			return cardSetCache ?? throw new KeyNotFoundException($"CardSet {CardSetId} not found.");
		}
	}
	private CardSet? cardSetCache;

	[JsonIgnore]
	public CardSearchData CardSearchData => cardSearchDataCache ??= $"{CardSet.CardSetSearchData} {Id} {Name} {Rarity} {RegulationMark} {Supertype} {string.Join(' ', Subtypes.Distinct())} {string.Join(' ', Types.Distinct())} {Number} {Artist} {FlavorText} {string.Join(' ', NationalPokedexNumbers)}".As<CardSearchData>();
	private CardSearchData? cardSearchDataCache;
}
