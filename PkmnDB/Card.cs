namespace ktsu.PkmnDB;

using System.Collections.Generic;

public class Card
{
	public string Id { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Supertype { get; set; } = string.Empty;
	public List<string> Subtypes { get; set; } = [];
	public string Level { get; set; } = string.Empty;
	public string Hp { get; set; } = string.Empty;
	public List<string> Types { get; set; } = [];
	public string EvolvesFrom { get; set; } = string.Empty;
	public List<Ability> Abilities { get; set; } = [];
	public List<Attack> Attacks { get; set; } = [];
	public List<Weakness> Weaknesses { get; set; } = [];
	public List<Resistance> Resistances { get; set; } = [];
	public List<string> RetreatCost { get; set; } = [];
	public int ConvertedRetreatCost { get; set; }
	public string Number { get; set; } = string.Empty;
	public string Artist { get; set; } = string.Empty;
	public string Rarity { get; set; } = string.Empty;
	public string FlavorText { get; set; } = string.Empty;
	public List<int> NationalPokedexNumbers { get; set; } = [];
	public Legalities Legalities { get; set; } = new();
	public CardImages Images { get; set; } = new();
}
