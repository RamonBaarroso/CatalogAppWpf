using System.Collections.ObjectModel;
using System.ComponentModel;
using ProductCatalog.Core.DTOs;
using ProductCatalog.Core.Interfaces;
using ProductCatalogApp.Views;
using System.Windows.Input;
using ProductCatalog.Core.Services;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;
using ProductCatalogApp.Commans;
using System.Diagnostics;

namespace ProductCatalogApp.ViewModels
{
  public class ProductListViewModel : INotifyPropertyChanged
  {
    private readonly IProductService _productService;
    private bool _isLoading;

    public ObservableCollection<ProductListDto> Products { get; } = new();

    public bool IsLoading
    {
      get => _isLoading;
      set
      {
        if (_isLoading != value)
        {
          _isLoading = value;
          OnPropertyChanged();
        }
      }
    }

    public ProductListViewModel(IProductService productService)
    {
      _productService = productService ?? throw new ArgumentNullException(nameof(productService));

      _ = LoadProductsAsync();
    }

    public async Task LoadProductsAsync()
    {
      try
      {
        IsLoading = true;
        Products.Clear();

        var products = await _productService.GetAllAsync();
        foreach (var product in products)
        {
          Products.Add(product);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Erro ao carregar produtos: {ex.Message}", "Erro",
                      MessageBoxButton.OK, MessageBoxImage.Error);
      }
      finally
      {
        IsLoading = false;
      }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }


}