namespace ProductCatalog.Core.Services
{

  public interface IJsonCacheService
  {
    Task SaveFavoritesAsync(List<int> productIds);
    Task<List<int>> LoadFavoritesAsync();
  }

}
