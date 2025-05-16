using System.Windows;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Core.Services;

namespace ProductCatalogApp
{
  public partial class FavoritesWindow : Window
  {
    public FavoritesWindow(IProductService productService, IJsonCacheService cacheService)
    {
      InitializeComponent();
      Loaded += async (s, e) =>
      {
        try
        {
          var favoriteIds = await cacheService.LoadFavoritesAsync();
          var tasks = favoriteIds.Select(id => productService.GetFavoriteByIdAsync(id));
          var results = await Task.WhenAll(tasks);
          FavoritesList.ItemsSource = results.Where(p => p != null).ToList();
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Erro ao carregar favoritos: {ex.Message}");
        }
      };
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
