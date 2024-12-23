namespace ktsu.PkmnDB;

using ktsu.StrongStrings;

public sealed record CardSetId : StrongStringAbstract<CardSetId> { }
public sealed record CardSetName : StrongStringAbstract<CardSetName> { }
public sealed record CardSetCode : StrongStringAbstract<CardSetCode> { }
public sealed record SeriesName : StrongStringAbstract<SeriesName> { }

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
}
