using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProductCatalog.Core.DTOs;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Core.Services;
using ProductCatalog.Infrastructure.Services;
using ProductCatalogApp.Commans;
using ProductCatalogApp.ViewModels;
using ProductCatalogApp.Views;

namespace ProductCatalogApp
{
  public partial class MainWindow : Window
  {
    private readonly IProductService _productService;
    private readonly IJsonCacheService _cacheService;

    public MainWindow(IProductService productService, IJsonCacheService cacheService)
    {

      InitializeComponent();

      _productService = productService;
      _cacheService = cacheService;


      DataContext = new ProductListViewModel(productService);

      Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      var products = await _productService.GetAllAsync();
      if (DataContext is ProductListViewModel vm)
      {
        vm.Products.Clear();
        foreach (var product in products)
        {
          vm.Products.Add(product);
        }
      }
    }

    private async void ViewDetails_Click(object sender, RoutedEventArgs e)
    {
      if (sender is Button btn && btn.Tag is ProductListDto product)
      {
        var detail = await _productService.GetDetailsByIdAsync(product.Id);
        if (detail != null)
        {
          var detailWindow = new ProductDetailsWindow(detail, _cacheService);
          detailWindow.ShowDialog();
        }
      }
    }

    private void OpenFavorite_Click(object sender, RoutedEventArgs e)
    {
      var window = new FavoritesWindow(_productService, _cacheService);
      window.ShowDialog();
    }

  }
}
