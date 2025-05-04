// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

/// <summary>
/// Contains URLs for the images associated with a card.
/// </summary>
public class CardImages
{
	/// <summary>
	/// Gets or sets the URL for the small version of the card image.
	/// </summary>
	public Uri Small { get; set; } = new("");

	/// <summary>
	/// Gets or sets the URL for the large version of the card image.
	/// </summary>
	public Uri Large { get; set; } = new("");
}
