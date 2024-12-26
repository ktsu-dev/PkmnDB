namespace ktsu.PkmnDB;

using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using ktsu.Extensions;
using ktsu.StrongPaths;
using ktsu.ToStringJsonConverter;
using ktsu.TextFilter;

/// <summary>
/// Provides methods to load and access Pokémon card data, card sets, and decks.
/// </summary>
public static class PkmnDB
{
	private static Lazy<Dictionary<CardSetId, CardSet>> CardSetsLazy { get; } = new(LoadCardSets);
	internal static Dictionary<CardSetId, CardSet> CardSets => CardSetsLazy.Value;

	private static Lazy<Dictionary<CardId, Card>> CardsLazy { get; } = new(LoadCards);
	internal static Dictionary<CardId, Card> Cards => CardsLazy.Value;

	private static Lazy<Dictionary<DeckId, Deck>> DecksLazy { get; } = new(LoadDecks);
	internal static Dictionary<DeckId, Deck> Decks => DecksLazy.Value;

	private static RelativeDirectoryPath CardSetDataPath { get; } = "data/sets".As<RelativeDirectoryPath>();
	private static RelativeDirectoryPath CardDataPath { get; } = "data/cards".As<RelativeDirectoryPath>();
	private static RelativeDirectoryPath DeckDataPath { get; } = "data/decks".As<RelativeDirectoryPath>();

	private static JsonSerializerOptions JsonSerializerOptions { get; } = new(JsonSerializerDefaults.General)
	{
		WriteIndented = true,
		IncludeFields = true,
		ReferenceHandler = ReferenceHandler.Preserve,
		Converters =
		{
			new JsonStringEnumConverter(),
			new ToStringJsonConverterFactory(),
		}
	};

	/// <summary>
	/// Loads all card sets from the data path.
	/// </summary>
	/// <returns>A dictionary of card sets indexed by their IDs.</returns>
	private static Dictionary<CardSetId, CardSet> LoadCardSets()
	{
		Dictionary<CardSetId, CardSet> cardSets = [];
		var cardSetFiles = CardSetDataPath.Contents;
		foreach (var cardSetFile in cardSetFiles)
		{
			string jsonString = File.ReadAllText(cardSetFile);
			var cardSet = JsonSerializer.Deserialize<CardSet>(jsonString, JsonSerializerOptions)!;
			cardSets[cardSet.Id] = cardSet;
		}

		return cardSets;
	}

	/// <summary>
	/// Loads all cards from the data path.
	/// </summary>
	/// <returns>A dictionary of cards indexed by their IDs.</returns>
	private static Dictionary<CardId, Card> LoadCards()
	{
		Dictionary<CardId, Card> cards = [];
		var cardFiles = CardDataPath.Contents;
		foreach (var cardFile in cardFiles)
		{
			string jsonString = File.ReadAllText(cardFile);
			var card = JsonSerializer.Deserialize<Card>(jsonString, JsonSerializerOptions)!;
			cards[card.Id] = card;
		}

		return cards;
	}

	/// <summary>
	/// Loads all decks from the data path.
	/// </summary>
	/// <returns>A dictionary of decks indexed by their IDs.</returns>
	private static Dictionary<DeckId, Deck> LoadDecks()
	{
		Dictionary<DeckId, Deck> decks = [];
		var deckFiles = DeckDataPath.Contents;
		foreach (var deckFile in deckFiles)
		{
			string jsonString = File.ReadAllText(deckFile);
			var deck = JsonSerializer.Deserialize<Deck>(jsonString, JsonSerializerOptions)!;
			decks[deck.Id] = deck;
		}

		return decks;
	}

	/// <summary>
	/// Gets all cards.
	/// </summary>
	/// <returns>A collection of all cards.</returns>
	public static Collection<Card> GetAllCards() => Cards.Values.ToCollection();

	/// <summary>
	/// Gets all card sets.
	/// </summary>
	/// <returns>A collection of all card sets.</returns>
	public static Collection<CardSet> GetAllCardSets() => CardSets.Values.ToCollection();

	/// <summary>
	/// Gets all decks.
	/// </summary>
	/// <returns>A collection of all decks.</returns>
	public static Collection<Deck> GetAllDecks() => Decks.Values.ToCollection();

	/// <summary>
	/// Gets a card set by its ID.
	/// </summary>
	/// <param name="id">The ID of the card set.</param>
	/// <returns>The card set with the specified ID.</returns>
	/// <exception cref="KeyNotFoundException">Thrown when the card set with the specified ID is not found.</exception>
	public static CardSet GetCardSet(CardSetId id) =>
		CardSets.TryGetValue(id, out var cardSet)
			? cardSet
			: throw new KeyNotFoundException($"Card set with id {id} not found.");

	/// <summary>
	/// Gets a card by its ID.
	/// </summary>
	/// <param name="id">The ID of the card.</param>
	/// <returns>The card with the specified ID.</returns>
	/// <exception cref="KeyNotFoundException">Thrown when the card with the specified ID is not found.</exception>
	public static Card GetCard(CardId id) =>
		Cards.TryGetValue(id, out var card)
			? card
			: throw new KeyNotFoundException($"Card with id {id} not found.");

	/// <summary>
	/// Gets a deck by its ID.
	/// </summary>
	/// <param name="id">The ID of the deck.</param>
	/// <returns>The deck with the specified ID.</returns>
	/// <exception cref="KeyNotFoundException">Thrown when the deck with the specified ID is not found.</exception>
	public static Deck GetDeck(DeckId id) =>
		Decks.TryGetValue(id, out var deck)
			? deck
			: throw new KeyNotFoundException($"Deck with id {id} not found.");

	/// <summary>
	/// Searches for cards that match the given query.
	/// </summary>
	/// <param name="query">The search query.</param>
	/// <returns>An enumerable of cards that match the query.</returns>
	public static IEnumerable<Card> SearchCards(string query) =>
		TextFilter.Rank(Cards.Values, c => c.CardSearchData, query);
}
