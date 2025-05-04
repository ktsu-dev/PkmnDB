// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.PkmnDB;

/// <summary>
/// Contains URLs for the images associated with a card set.
/// </summary>
public class SetImages
{
	/// <summary>
	/// Gets or sets the URL for the set symbol image.
	/// </summary>
	public Uri Symbol { get; set; } = new("");

	/// <summary>
	/// Gets or sets the URL for the set logo image.
	/// </summary>
	public Uri Logo { get; set; } = new("");
}
