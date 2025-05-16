using ProductCatalog.Core.DTOs;

namespace ProductCatalog.Core.Interfaces
{

  public interface IProductService
  {
    Task<IEnumerable<ProductListDto>> GetAllAsync();
    Task<ProductDetailDto?> GetDetailsByIdAsync(int id);

    Task<FavoriteProductDto?> GetFavoriteByIdAsync(int id);
  }

}