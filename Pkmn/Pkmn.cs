// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Pkmn;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ImGuiNET;
using ktsu.ImGuiApp;
using ktsu.ImGuiWidgets;
using ktsu.PkmnDB;
using ktsu.TextFilter;

/// <summary>
/// Main Pokemon Card Database GUI application
/// </summary>
public class PkmnApp : IDisposable
{
	// Readonly fields for core components
	private readonly ImGuiWidgets.TabPanel _tabPanel;
	private readonly CardImageLoader _imageLoader;
	private readonly string _cardsTabId;
	private readonly string _setsTabId;
	private readonly string _decksTabId;
	private bool _disposed;

	// Fields for search terms
	private string _cardSearchTerm = string.Empty;
	private string _setSearchTerm = string.Empty;
	private string _deckSearchTerm = string.Empty;

	/// <summary>
	/// Currently selected card
	/// </summary>
	public Card? SelectedCard { get; private set; }

	/// <summary>
	/// Currently selected card set
	/// </summary>
	public CardSet? SelectedSet { get; private set; }

	/// <summary>
	/// Currently selected deck
	/// </summary>
	public Deck? SelectedDeck { get; private set; }

	/// <summary>
	/// Main entry point for the application
	/// </summary>
	public static void Main()
	{
		using var app = new PkmnApp();
		ImGuiApp.Start(new()
		{
			Title = "Pokemon TCG Database",
			OnRender = app.Render
		});
	}

	/// <summary>
	/// Constructor for the Pokemon application
	/// </summary>
	public PkmnApp()
	{
		// Create image loader
		_imageLoader = new CardImageLoader(new ImGuiAppRenderer(null));

		// Create tab panel with closable tabs set to false, reorderable tabs set to true
		_tabPanel = new ImGuiWidgets.TabPanel("MainTabBar", false, true);

		// Add tabs - use empty Action delegate instead of null
		_cardsTabId = _tabPanel.AddTab("cards", "Cards", () => { });
		_setsTabId = _tabPanel.AddTab("sets", "Sets", () => { });
		_decksTabId = _tabPanel.AddTab("decks", "Decks", () => { });
	}

	/// <summary>
	/// Render the GUI
	/// </summary>
	private void Render(float deltaTime)
	{
		ImGui.DockSpaceOverViewport();

		if (ImGui.BeginMainMenuBar())
		{
			if (ImGui.BeginMenu("File"))
			{
				if (ImGui.MenuItem("Exit"))
				{
					ImGuiApp.Stop();
				}

				ImGui.EndMenu();
			}

			if (ImGui.BeginMenu("Help"))
			{
				if (ImGui.MenuItem("About"))
				{
					// Display about info
				}

				ImGui.EndMenu();
			}

			ImGui.EndMainMenuBar();
		}

		// Draw the tab panel
		_tabPanel.Draw();
		var activeTabId = _tabPanel.ActiveTabId;

		// Render content based on the active tab
		if (activeTabId == _cardsTabId)
		{
			RenderCardsTab();
		}
		else if (activeTabId == _setsTabId)
		{
			RenderSetsTab();
		}
		else if (activeTabId == _decksTabId)
		{
			RenderDecksTab();
		}
	}

	private void RenderCardsTab()
	{
		ImGui.PushID("CardsTab");

		// Create a two-panel layout
		if (ImGui.BeginTable("CardsTabLayout", 2, ImGuiTableFlags.Resizable))
		{
			ImGui.TableSetupColumn("Search", ImGuiTableColumnFlags.WidthFixed, 300);
			ImGui.TableSetupColumn("Details", ImGuiTableColumnFlags.WidthStretch);

			ImGui.TableNextRow();

			// Left panel: Search and list
			ImGui.TableNextColumn();

			// Use SearchBox API
			var allCards = PkmnDB.GetAllCards();

			var filterType = TextFilterType.Fuzzy;
			var matchOptions = TextFilterMatchOptions.ByWordAny;

			var filteredCards = ImGuiWidgets.SearchBox(
				"##CardSearch",
				ref _cardSearchTerm,
				allCards,
				card => card.CardSearchData,
				ref filterType,
				ref matchOptions);

			// Display filtered cards in a list
			if (ImGui.BeginChild("CardsList", new Vector2(0, -ImGui.GetFrameHeightWithSpacing())))
			{
				foreach (var card in filteredCards)
				{
					if (ImGui.Selectable($"{card.Name} - {card.Number} ({card.CardSet?.Name ?? "Unknown Set"})", SelectedCard == card))
					{
						SelectedCard = card;
					}
				}

				ImGui.EndChild();
			}

			// Right panel: Card details
			ImGui.TableNextColumn();

			if (SelectedCard != null)
			{
				var card = SelectedCard;
				if (ImGui.BeginChild("CardDetails"))
				{
					// Display card image if available
					var textureId = _imageLoader.GetCardImageTextureId(card);
					if (textureId > 0)
					{
						var maxWidth = ImGui.GetContentRegionAvail().X;
						var aspectRatio = 1.39f; // Standard Pokemon card ratio (63mm x 88mm)
						var width = Math.Min(300, maxWidth);
						var height = width * aspectRatio;

						ImGui.Image(textureId, new Vector2(width, height));
						ImGui.Separator();
					}

					if (ImGui.BeginTable("CardDetailsTable", 2))
					{
						ImGui.TableSetupColumn("Property", ImGuiTableColumnFlags.WidthFixed, 150);
						ImGui.TableSetupColumn("Value", ImGuiTableColumnFlags.WidthStretch);

						RenderTableRow("Name", card.Name.ToString());
						RenderTableRow("Number", card.Number.ToString());
						RenderTableRow("Set", card.CardSet?.Name.ToString() ?? "Unknown");
						RenderTableRow("Supertype", card.Supertype.ToString());
						RenderTableRow("Subtypes", string.Join(", ", card.Subtypes.Select(st => st.ToString())));
						RenderTableRow("HP", card.Hp.ToString());
						RenderTableRow("Types", string.Join(", ", card.Types.Select(t => t.ToString())));
						RenderTableRow("Artist", card.Artist.ToString());
						RenderTableRow("Rarity", card.Rarity.ToString());

						if (card.Attacks.Count > 0)
						{
							ImGui.TableNextRow();
							ImGui.TableNextColumn();
							ImGui.Text("Attacks");
							ImGui.TableNextColumn();

							foreach (var attack in card.Attacks)
							{
								ImGui.Text($"{attack.Name} - {attack.Damage} - {attack.Text}");
							}
						}

						if (!string.IsNullOrEmpty(card.FlavorText.ToString()))
						{
							RenderTableRow("Flavor Text", card.FlavorText.ToString());
						}

						ImGui.EndTable();
					}

					ImGui.EndChild();
				}
			}
			else
			{
				ImGui.TextDisabled("Select a card to view details");
			}

			ImGui.EndTable();
		}

		ImGui.PopID();
	}

	private void RenderSetsTab()
	{
		ImGui.PushID("SetsTab");

		// Create a two-panel layout
		if (ImGui.BeginTable("SetsTabLayout", 2, ImGuiTableFlags.Resizable))
		{
			ImGui.TableSetupColumn("Search", ImGuiTableColumnFlags.WidthFixed, 300);
			ImGui.TableSetupColumn("Details", ImGuiTableColumnFlags.WidthStretch);

			ImGui.TableNextRow();

			// Left panel: Search and list
			ImGui.TableNextColumn();

			// Use SearchBox API
			var allSets = PkmnDB.GetAllCardSets();

			var filterType = TextFilterType.Fuzzy;
			var matchOptions = TextFilterMatchOptions.ByWordAny;

			var filteredSets = ImGuiWidgets.SearchBox(
				"##SetSearch",
				ref _setSearchTerm,
				allSets,
				set => set.CardSetSearchData,
				ref filterType,
				ref matchOptions);

			// Display filtered sets in a list
			if (ImGui.BeginChild("SetsList", new Vector2(0, -ImGui.GetFrameHeightWithSpacing())))
			{
				foreach (var set in filteredSets)
				{
					if (ImGui.Selectable($"{set.Name} ({set.Series})", SelectedSet == set))
					{
						SelectedSet = set;
					}
				}

				ImGui.EndChild();
			}

			// Right panel: Set details
			ImGui.TableNextColumn();

			if (SelectedSet != null)
			{
				var set = SelectedSet;
				if (ImGui.BeginChild("SetDetails"))
				{
					// Display set logo if available
					var textureId = _imageLoader.GetSetIconTextureId(set);
					if (textureId > 0)
					{
						ImGui.Image(textureId, new Vector2(50, 50));
						ImGui.SameLine();
					}

					ImGui.BeginGroup();
					ImGui.Text($"{set.Name}");
					ImGui.Text($"{set.Series}");
					ImGui.EndGroup();

					ImGui.Separator();

					if (ImGui.BeginTable("SetDetailsTable", 2))
					{
						ImGui.TableSetupColumn("Property", ImGuiTableColumnFlags.WidthFixed, 150);
						ImGui.TableSetupColumn("Value", ImGuiTableColumnFlags.WidthStretch);

						RenderTableRow("Total Cards", set.Total.ToString());
						RenderTableRow("Printed Total", set.PrintedTotal.ToString());
						RenderTableRow("PTCGO Code", set.PtcgoCode.ToString());
						RenderTableRow("Release Date", set.ReleaseDate.ToShortDateString());
						RenderTableRow("Updated At", set.UpdatedAt.ToShortDateString());

						ImGui.EndTable();
					}

					// List cards in this set
					ImGui.Separator();
					ImGui.Text("Cards in this set:");

					if (ImGui.BeginChild("SetCards", new Vector2(0, 0), ImGuiChildFlags.None))
					{
						var cardsInSet = PkmnDB.GetAllCards()
							.Where(c => c.CardSetId.ToString() == set.Id.ToString())
							.OrderBy(c => c.Number.ToString());

						foreach (var card in cardsInSet)
						{
							if (ImGui.Selectable($"{card.Number} - {card.Name}", SelectedCard == card))
							{
								SelectedCard = card;
							}
						}

						ImGui.EndChild();
					}

					ImGui.EndChild();
				}
			}
			else
			{
				ImGui.TextDisabled("Select a set to view details");
			}

			ImGui.EndTable();
		}

		ImGui.PopID();
	}

	private void RenderDecksTab()
	{
		ImGui.PushID("DecksTab");

		// Create a two-panel layout
		if (ImGui.BeginTable("DecksTabLayout", 2, ImGuiTableFlags.Resizable))
		{
			ImGui.TableSetupColumn("Search", ImGuiTableColumnFlags.WidthFixed, 300);
			ImGui.TableSetupColumn("Details", ImGuiTableColumnFlags.WidthStretch);

			ImGui.TableNextRow();

			// Left panel: Search and list
			ImGui.TableNextColumn();

			// Use SearchBox API
			var allDecks = PkmnDB.GetAllDecks();

			var filterType = TextFilterType.Fuzzy;
			var matchOptions = TextFilterMatchOptions.ByWordAny;

			var filteredDecks = ImGuiWidgets.SearchBox(
				"##DeckSearch",
				ref _deckSearchTerm,
				allDecks,
				deck => deck.DeckSearchData,
				ref filterType,
				ref matchOptions);

			// Display filtered decks in a list
			if (ImGui.BeginChild("DecksList", new Vector2(0, -ImGui.GetFrameHeightWithSpacing())))
			{
				foreach (var deck in filteredDecks)
				{
					if (ImGui.Selectable(deck.Name.ToString(), SelectedDeck == deck))
					{
						SelectedDeck = deck;
					}
				}

				ImGui.EndChild();
			}

			// Right panel: Deck details
			ImGui.TableNextColumn();

			if (SelectedDeck != null)
			{
				var deck = SelectedDeck;
				if (ImGui.BeginChild("DeckDetails"))
				{
					ImGui.Text($"Deck: {deck.Name}");
					ImGui.Text($"Types: {string.Join(", ", deck.Types.Select(t => t.ToString()))}");
					ImGui.Separator();

					ImGui.Text("Cards:");

					if (ImGui.BeginTable("DeckCards", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
					{
						ImGui.TableSetupColumn("Count", ImGuiTableColumnFlags.WidthFixed, 50);
						ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.WidthStretch);
						ImGui.TableSetupColumn("ID", ImGuiTableColumnFlags.WidthFixed, 150);
						ImGui.TableHeadersRow();

						foreach (var card in deck.Cards)
						{
							ImGui.TableNextRow();

							ImGui.TableNextColumn();
							ImGui.Text(card.Count.ToString());

							ImGui.TableNextColumn();
							if (ImGui.Selectable(card.Name.ToString(), false, ImGuiSelectableFlags.SpanAllColumns))
							{
								try
								{
									SelectedCard = PkmnDB.GetCard(card.Id);
								}
								catch (KeyNotFoundException)
								{
									// Card not found
								}
							}

							ImGui.TableNextColumn();
							ImGui.Text(card.Id.ToString());
						}

						ImGui.EndTable();
					}

					ImGui.EndChild();
				}
			}
			else
			{
				ImGui.TextDisabled("Select a deck to view details");
			}

			ImGui.EndTable();
		}

		ImGui.PopID();
	}

	private static void RenderTableRow(string property, string value)
	{
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		ImGui.Text(property);
		ImGui.TableNextColumn();
		ImGui.Text(value);
	}

	/// <summary>
	/// Dispose resources
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Dispose pattern implementation
	/// </summary>
	protected virtual void Dispose(bool disposing)
	{
		if (_disposed)
		{
			return;
		}

		if (disposing)
		{
			_imageLoader.Dispose();
		}

		_disposed = true;
	}
}
