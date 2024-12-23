namespace ktsu.PkmnDB;

using System.Collections.Generic;

public class Attack
{
	public string Name { get; set; } = string.Empty;
	public List<string> Cost { get; set; } = [];
	public int ConvertedEnergyCost { get; set; }
	public string Damage { get; set; } = string.Empty;
	public string Text { get; set; } = string.Empty;
}
