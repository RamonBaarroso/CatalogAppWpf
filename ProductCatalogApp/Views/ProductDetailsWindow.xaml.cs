using System.Windows;
using ProductCatalog.Core.DTOs;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Core.Services;
using ProductCatalog.Infrastructure.Services;
using ProductCatalogApp.ViewModels;

namespace ProductCatalogApp.Views
{
  /// <summary>
  /// Interaction logic for ProductDetailsWindow.xaml
  /// </summary>
  public partial class ProductDetailsWindow : Window
  {
    private readonly IJsonCacheService _cacheService;
    public ProductDetailsWindow(ProductDetailDto productDetailDto, IJsonCacheService cacheService)
    {
      InitializeComponent();
      DataContext = productDetailDto;
      _cacheService = cacheService;
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private async void Favorite_Click(object sender, RoutedEventArgs e)
    {
      if (DataContext is ProductDetailDto product)
        await ToggleFavorite(product);
    }

    private async Task ToggleFavorite(ProductDetailDto product)
    {
      var favorites = await _cacheService.LoadFavoritesAsync();

      if (favorites.Contains(product.Id))
        favorites.Remove(product.Id);
      else
        favorites.Add(product.Id);

      await _cacheService.SaveFavoritesAsync(favorites);
    }
  }


}
