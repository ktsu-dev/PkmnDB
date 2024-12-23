namespace ktsu.PkmnDB;

using ktsu.StrongStrings;

public sealed record DeckId : StrongStringAbstract<DeckId> { }
public sealed record DeckName : StrongStringAbstract<DeckName> { }

public class Deck
{
	public DeckId Id { get; set; } = new();
	public DeckName Name { get; set; } = new();
	public List<CardType> Types { get; set; } = [];
	public List<DeckCard> Cards { get; set; } = [];
}
