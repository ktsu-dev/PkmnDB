// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Pkmn;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ktsu.ImGuiApp;
using ktsu.PkmnDB;

/// <summary>
/// Helper class for loading and caching card images
/// </summary>
/// <remarks>
/// Initializes a new instance of the CardImageLoader class
/// </remarks>
public class CardImageLoader() : IDisposable
{
	private readonly HttpClient _httpClient = new();
	private readonly Dictionary<string, int> _imageTextureCache = [];
	private readonly Queue<string> _pendingImageUrls = new();
	private readonly HashSet<string> _processingUrls = [];
	private readonly Lock _syncLock = new();
	private bool _isProcessingQueue;
	private bool _disposed;

	/// <summary>
	/// Gets the texture ID for a card image
	/// </summary>
	/// <param name="card">The card to get the image for</param>
	/// <param name="useSmallImage">Whether to use the small image instead of the large one</param>
	/// <returns>The texture ID if available, or 0 if the image is not yet loaded</returns>
	public int GetCardImageTextureId(Card card, bool useSmallImage = false)
	{
		if (card == null)
		{
			return 0;
		}

		var imageUrl = (useSmallImage ? card.Images.Small : card.Images.Large)?.ToString() ?? string.Empty;
		if (string.IsNullOrEmpty(imageUrl))
		{
			return 0;
		}

		lock (_syncLock)
		{
			if (_imageTextureCache.TryGetValue(imageUrl, out var textureId))
			{
				return textureId;
			}

			// Queue the image for loading if it's not already being processed
			if (!_processingUrls.Contains(imageUrl))
			{
				_pendingImageUrls.Enqueue(imageUrl);
				_processingUrls.Add(imageUrl);
				StartProcessingQueue();
			}

			return 0;
		}
	}

	/// <summary>
	/// Gets the texture ID for a card set icon
	/// </summary>
	/// <param name="set">The card set to get the icon for</param>
	/// <returns>The texture ID if available, or 0 if the image is not yet loaded</returns>
	public int GetSetIconTextureId(CardSet set)
	{
		if (set == null)
		{
			return 0;
		}

		var imageUrl = set.Images.Symbol?.ToString() ?? string.Empty;
		if (string.IsNullOrEmpty(imageUrl))
		{
			return 0;
		}

		lock (_syncLock)
		{
			if (_imageTextureCache.TryGetValue(imageUrl, out var textureId))
			{
				return textureId;
			}

			// Queue the image for loading if it's not already being processed
			if (!_processingUrls.Contains(imageUrl))
			{
				_pendingImageUrls.Enqueue(imageUrl);
				_processingUrls.Add(imageUrl);
				StartProcessingQueue();
			}

			return 0;
		}
	}

	private void StartProcessingQueue()
	{
		lock (_syncLock)
		{
			if (_isProcessingQueue)
			{
				return;
			}

			_isProcessingQueue = true;
		}

		Task.Run(ProcessImageQueue);
	}

	private async Task ProcessImageQueue()
	{
		try
		{
			while (true)
			{
				string? imageUrl;

				lock (_syncLock)
				{
					if (_pendingImageUrls.Count == 0)
					{
						_isProcessingQueue = false;
						return;
					}

					imageUrl = _pendingImageUrls.Dequeue();
				}

				try
				{
					var imageData = await _httpClient.GetByteArrayAsync(new Uri(imageUrl)).ConfigureAwait(false);
					var textureId = await ImGuiApp..CreateTextureAsync(imageData).ConfigureAwait(false);

					lock (_syncLock)
					{
						_imageTextureCache[imageUrl] = textureId;
					}
				}
				catch (HttpRequestException ex)
				{
					Console.WriteLine($"Failed to load image {imageUrl}: {ex.Message}");

					lock (_syncLock)
					{
						_processingUrls.Remove(imageUrl);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in image processing queue: {ex.Message}");

			lock (_syncLock)
			{
				_isProcessingQueue = false;
			}
		}
	}

	/// <summary>
	/// Disposes of all cached textures
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Dispose pattern implementation
	/// </summary>
	/// <param name="disposing">Whether this is being called from Dispose() or finalizer</param>
	protected virtual void Dispose(bool disposing)
	{
		if (_disposed)
		{
			return;
		}

		if (disposing)
		{
			foreach (var textureId in _imageTextureCache.Values)
			{
				_renderer.DestroyTexture(textureId);
			}

			_imageTextureCache.Clear();
			_httpClient.Dispose();
		}

		_disposed = true;
	}
}
