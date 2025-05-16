using ProductCatalog.Core.Domain.Entities;
using ProductCatalog.Core.DTOs;
using ProductCatalog.Core.Interfaces;
using ProductCatalogApp.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProductCatalogApp.ViewModels
{
  public class ProductDetailViewModel : INotifyPropertyChanged
  {
    private readonly ProductDetailDto _product;
    private readonly IProductService _productService;

    public ProductDetailViewModel(ProductDetailDto productDetailDto, IProductService productService)
    {
      LoadDetails(productDetailDto);
      _productService = productService;
    }

    public string Description => _product.Description;
    public string Category => _product.Category;

    public string Rating => $"{_product.Rating?.Rate ?? 0:F1} ({_product.Rating?.Count ?? 0} avaliações)";

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void LoadDetails(ProductDetailDto productDetailDto)
    {
      await _productService.GetDetailsByIdAsync(productDetailDto.Id);
    }

  }
}
