namespace ktsu.PkmnDB;

public class CardSet
{
	public string Id { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Series { get; set; } = string.Empty;
	public int PrintedTotal { get; set; }
	public int Total { get; set; }
	public Legalities Legalities { get; set; } = new();
	public string PtcgoCode { get; set; } = string.Empty;
	public DateTime ReleaseDate { get; set; }
	public DateTime UpdatedAt { get; set; }
	public SetImages Images { get; set; } = new();
}
