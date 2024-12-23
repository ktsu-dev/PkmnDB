namespace ktsu.PkmnDB;

public class Deck
{
	public string Id { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public List<string> Types { get; set; } = [];
	public List<DeckCard> Cards { get; set; } = [];
}
