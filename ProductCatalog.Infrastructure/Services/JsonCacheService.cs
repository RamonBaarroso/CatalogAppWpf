using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Core.Services;
using ProductCatalog.Core.Exceptions;

namespace ProductCatalog.Infrastructure.Services
{
  public class JsonCacheService : IJsonCacheService
  {
    private readonly string _filePath;

    public JsonCacheService()
    {
      var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      _filePath = Path.Combine(folder, "favorites_cache.json");
    }

    /// <summary>
    /// Função de salvar favoritos
    /// </summary>
    /// <param name="productIds"></param>
    /// <returns></returns>
    /// <exception cref="CacheException"></exception>
    public async Task SaveFavoritesAsync(List<int> productIds)
    {
      try
      {
        var json = JsonSerializer.Serialize(productIds, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
      }
      catch (Exception ex)
      {
        throw new CacheException("Falha ao salvar favoritos.", ex);
      }
    }

    /// <summary>
    /// Função de carregar favoritos salvos em cache
    /// </summary>
    /// <returns></returns>
    /// <exception cref="CacheException"></exception>
    public async Task<List<int>> LoadFavoritesAsync()
    {
      try
      {
        if (!File.Exists(_filePath))
          return new List<int>();

        var json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
      }
      catch (Exception ex)
      {
        throw new CacheException("Falha ao carregar favoritos", ex);
      }
    }
  }
}