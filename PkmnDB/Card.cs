// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using ktsu.Semantics;

/// <summary>
/// Represents a card identifier.
/// </summary>
public sealed record CardId : SemanticString<CardId> { }

/// <summary>
/// Represents a card name.
/// </summary>
public sealed record CardName : SemanticString<CardName> { }

/// <summary>
/// Represents a card supertype.
/// </summary>
public sealed record CardSupertype : SemanticString<CardSupertype> { }

/// <summary>
/// Represents a card subtype.
/// </summary>
public sealed record CardSubtype : SemanticString<CardSubtype> { }

/// <summary>
/// Represents a card level.
/// </summary>
public sealed record CardLevel : SemanticString<CardLevel> { }

/// <summary>
/// Represents a card's HP value.
/// </summary>
public sealed record CardHp : SemanticString<CardHp> { }

/// <summary>
/// Represents a card type.
/// </summary>
public sealed record CardType : SemanticString<CardType> { }

/// <summary>
/// Represents a card retreat cost.
/// </summary>
public sealed record CardRetreatCost : SemanticString<CardRetreatCost> { }

/// <summary>
/// Represents a card number.
/// </summary>
public sealed record CardNumber : SemanticString<CardNumber> { }

/// <summary>
/// Represents a card artist.
/// </summary>
public sealed record CardArtist : SemanticString<CardArtist> { }

/// <summary>
/// Represents a card rarity.
/// </summary>
public sealed record CardRarity : SemanticString<CardRarity> { }

/// <summary>
/// Represents a card flavor text.
/// </summary>
public sealed record CardFlavorText : SemanticString<CardFlavorText> { }

/// <summary>
/// Represents a card regulation mark.
/// </summary>
public sealed record CardRegulationMark : SemanticString<CardRegulationMark> { }

/// <summary>
/// Represents card search data.
/// </summary>
public sealed record CardSearchData : SemanticString<CardSearchData> { }

/// <summary>
/// Represents a Pokemon Trading Card Game card.
/// </summary>
public class Card
{
	private readonly List<CardSubtype> _subtypes = [];
	private readonly List<CardType> _types = [];
	private readonly List<Ability> _abilities = [];
	private readonly List<Attack> _attacks = [];
	private readonly List<Weakness> _weaknesses = [];
	private readonly List<Resistance> _resistances = [];
	private readonly List<CardRetreatCost> _retreatCost = [];
	private readonly List<int> _nationalPokedexNumbers = [];

	/// <summary>
	/// Gets or sets the unique identifier for the card.
	/// </summary>
	public CardId Id { get; set; } = new();

	/// <summary>
	/// Gets or sets the name of the card.
	/// </summary>
	public CardName Name { get; set; } = new();

	/// <summary>
	/// Gets or sets the supertype of the card (e.g., Pokémon, Trainer, Energy).
	/// </summary>
	public CardSupertype Supertype { get; set; } = new();

	/// <summary>
	/// Gets the collection of subtypes for the card.
	/// </summary>
	public IReadOnlyCollection<CardSubtype> Subtypes => _subtypes.AsReadOnly();

	/// <summary>
	/// Gets or sets the level of the card.
	/// </summary>
	public CardLevel Level { get; set; } = new();

	/// <summary>
	/// Gets or sets the HP (Hit Points) of the card.
	/// </summary>
	public CardHp Hp { get; set; } = new();

	/// <summary>
	/// Gets the collection of types for the card.
	/// </summary>
	public IReadOnlyCollection<CardType> Types => _types.AsReadOnly();

	/// <summary>
	/// Gets or sets the name of the card this card evolves from.
	/// </summary>
	public CardName EvolvesFrom { get; set; } = new();

	/// <summary>
	/// Gets the collection of abilities for the card.
	/// </summary>
	public IReadOnlyCollection<Ability> Abilities => _abilities.AsReadOnly();

	/// <summary>
	/// Gets the collection of attacks for the card.
	/// </summary>
	public IReadOnlyCollection<Attack> Attacks => _attacks.AsReadOnly();

	/// <summary>
	/// Gets the collection of weaknesses for the card.
	/// </summary>
	public IReadOnlyCollection<Weakness> Weaknesses => _weaknesses.AsReadOnly();

	/// <summary>
	/// Gets the collection of resistances for the card.
	/// </summary>
	public IReadOnlyCollection<Resistance> Resistances => _resistances.AsReadOnly();

	/// <summary>
	/// Gets the collection of retreat costs for the card.
	/// </summary>
	public IReadOnlyCollection<CardRetreatCost> RetreatCost => _retreatCost.AsReadOnly();

	/// <summary>
	/// Gets or sets the converted retreat cost of the card.
	/// </summary>
	public int ConvertedRetreatCost { get; set; }

	/// <summary>
	/// Gets or sets the number of the card in the set.
	/// </summary>
	public CardNumber Number { get; set; } = new();

	/// <summary>
	/// Gets or sets the artist of the card.
	/// </summary>
	public CardArtist Artist { get; set; } = new();

	/// <summary>
	/// Gets or sets the rarity of the card.
	/// </summary>
	public CardRarity Rarity { get; set; } = new();

	/// <summary>
	/// Gets or sets the flavor text of the card.
	/// </summary>
	public CardFlavorText FlavorText { get; set; } = new();

	/// <summary>
	/// Gets the collection of National Pokedex numbers for the card.
	/// </summary>
	public IReadOnlyCollection<int> NationalPokedexNumbers => _nationalPokedexNumbers.AsReadOnly();

	/// <summary>
	/// Gets or sets the legalities information for the card.
	/// </summary>
	public Legalities Legalities { get; set; } = new();

	/// <summary>
	/// Gets or sets the regulation mark of the card.
	/// </summary>
	public CardRegulationMark RegulationMark { get; set; } = new();

	/// <summary>
	/// Gets or sets the images associated with the card.
	/// </summary>
	public CardImages Images { get; set; } = new();

	/// <summary>
	/// Adds a subtype to the card.
	/// </summary>
	/// <param name="subtype">The subtype to add.</param>
	public void AddSubtype(CardSubtype subtype) => _subtypes.Add(subtype);

	/// <summary>
	/// Adds a type to the card.
	/// </summary>
	/// <param name="type">The type to add.</param>
	public void AddType(CardType type) => _types.Add(type);

	/// <summary>
	/// Adds an ability to the card.
	/// </summary>
	/// <param name="ability">The ability to add.</param>
	public void AddAbility(Ability ability) => _abilities.Add(ability);

	/// <summary>
	/// Adds an attack to the card.
	/// </summary>
	/// <param name="attack">The attack to add.</param>
	public void AddAttack(Attack attack) => _attacks.Add(attack);

	/// <summary>
	/// Adds a weakness to the card.
	/// </summary>
	/// <param name="weakness">The weakness to add.</param>
	public void AddWeakness(Weakness weakness) => _weaknesses.Add(weakness);

	/// <summary>
	/// Adds a resistance to the card.
	/// </summary>
	/// <param name="resistance">The resistance to add.</param>
	public void AddResistance(Resistance resistance) => _resistances.Add(resistance);

	/// <summary>
	/// Adds a retreat cost to the card.
	/// </summary>
	/// <param name="retreatCost">The retreat cost to add.</param>
	public void AddRetreatCost(CardRetreatCost retreatCost) => _retreatCost.Add(retreatCost);

	/// <summary>
	/// Adds a National Pokedex number to the card.
	/// </summary>
	/// <param name="number">The Pokedex number to add.</param>
	public void AddNationalPokedexNumber(int number) => _nationalPokedexNumbers.Add(number);

	/// <summary>
	/// Parses the set ID from the card ID.
	/// </summary>
	/// <returns>The card set ID.</returns>
	internal CardSetId ParseSetId() => Id.Split('-').First().As<CardSetId>();

	/// <summary>
	/// Gets the card set ID this card belongs to.
	/// </summary>
	[JsonIgnore]
	public CardSetId CardSetId => cardSetIdCache ??= ParseSetId();
	private CardSetId? cardSetIdCache;

	/// <summary>
	/// Gets the card set this card belongs to, or null if not found.
	/// </summary>
	[JsonIgnore]
	public CardSet? CardSet
	{
		get
		{
			if (cardSetCache is null)
			{
				PkmnDB.CardSets.TryGetValue(CardSetId, out cardSetCache);
			}

			return cardSetCache;
		}
	}
	private CardSet? cardSetCache;

	/// <summary>
	/// Gets the combined search data for the card.
	/// </summary>
	[JsonIgnore]
	public CardSearchData CardSearchData => cardSearchDataCache ??= $"{CardSet?.CardSetSearchData ?? string.Empty} {Id} {Name} {Rarity} {RegulationMark} {Supertype} {string.Join(' ', Subtypes.Distinct())} {string.Join(' ', Types.Distinct())} {Number} {Artist} {FlavorText} {string.Join(' ', NationalPokedexNumbers)}".As<CardSearchData>();
	private CardSearchData? cardSearchDataCache;
}
