namespace ktsu.PkmnDB;

using System.Collections.Generic;
using ktsu.StrongStrings;

public sealed record CardId : StrongStringAbstract<CardId> { }
public sealed record CardName : StrongStringAbstract<CardName> { }
public sealed record CardSupertype : StrongStringAbstract<CardSupertype> { }
public sealed record CardSubtype : StrongStringAbstract<CardSubtype> { }
public sealed record CardLevel : StrongStringAbstract<CardLevel> { }
public sealed record CardHp : StrongStringAbstract<CardHp> { }
public sealed record CardType : StrongStringAbstract<CardType> { }
public sealed record CardRetreatCost : StrongStringAbstract<CardRetreatCost> { }
public sealed record CardNumber : StrongStringAbstract<CardNumber> { }
public sealed record CardArtist : StrongStringAbstract<CardArtist> { }
public sealed record CardRarity : StrongStringAbstract<CardRarity> { }
public sealed record CardFlavorText : StrongStringAbstract<CardFlavorText> { }

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
	public CardImages Images { get; set; } = new();
}
