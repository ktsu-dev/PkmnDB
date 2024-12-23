namespace ktsu.PkmnDB;

public class DeckCard
{
	public CardId Id { get; set; } = new();
	public CardName Name { get; set; } = new();
	public CardRarity Rarity { get; set; } = new();
	public int Count { get; set; }
}
