using System.Net.Http.Json;
using ProductCatalog.Core.DTOs;
using ProductCatalog.Core.Interfaces;
using Polly;
using Polly.Retry;
using ProductCatalog.Core.Exceptions;
using Polly.Extensions.Http;
using ProductCatalog.Core.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductCatalog.Infrastructure.Services
{

  public class ProductService : IProductService
  {
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public ProductService(HttpClient httpClient)
    {
      _httpClient = httpClient;

      _retryPolicy = HttpPolicyExtensions
          .HandleTransientHttpError()
          .WaitAndRetryAsync(3, retryAttempt =>
              TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    /// <summary>
    /// Busca todos os produtos da API
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ProductServiceException"></exception>
    public async Task<IEnumerable<ProductListDto>> GetAllAsync()
    {
      try
      {
        var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(""));

        if (!response.IsSuccessStatusCode)
        {
          throw new ProductServiceException($"Falha ao carregar produtos.");
        }

        var products = await response.Content.ReadFromJsonAsync<List<ProductListDto>>();
        return products ?? new List<ProductListDto>();
      }

      catch (HttpRequestException ex)
      {
        throw new ProductServiceException("Erro de rede ao buscar produtos. Verifique sua conexão.", ex);
      }

      catch (Exception ex)
      {
        throw new ProductServiceException("Ocorreu um erro inesperado ao buscar produtos", ex);
      }
    }

    /// <summary>
    /// Traz os detalhes do produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ProductServiceException"></exception>
    public async Task<ProductDetailDto?> GetDetailsByIdAsync(int id)
    {
      try
      {
        var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync($"{id}"));

        if (!response.IsSuccessStatusCode)
        {
          throw new ProductServiceException($"Falha ao carregar os detalhes do produto para o ID {id}. Código de status: {response.StatusCode}");
        }

        var product = await response.Content.ReadFromJsonAsync<ProductDetailDto>();
        return product;
      }
      catch (HttpRequestException ex)
      {
        throw new ProductServiceException($"Erro de rede ao buscar detalhes do produto para o ID {id}. Verifique sua conexão.", ex);
      }
      catch (Exception ex)
      {
        throw new ProductServiceException($"Ocorreu um erro inesperado ao buscar detalhes do produto para o ID { id}.", ex);
      }
    }

    /// <summary>
    /// Busca o produto favoritado pelo usuario 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ProductServiceException"></exception>
    public async Task<FavoriteProductDto?> GetFavoriteByIdAsync(int id)
    {
      try
      {
        var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync($"{id}"));

        if (!response.IsSuccessStatusCode)
        {
          throw new ProductServiceException($"Falha ao carregar os detalhes do produto para o ID {id}. Código de status: {response.StatusCode}");
        }

        var product = await response.Content.ReadFromJsonAsync<FavoriteProductDto>();
        return product;
      }
      catch (HttpRequestException ex)
      {
        throw new ProductServiceException($"Erro de rede ao buscar detalhes do produto para o ID {id}. Verifique sua conexão.", ex);
      }
      catch (Exception ex)
      {
        throw new ProductServiceException($"Ocorreu um erro inesperado ao buscar detalhes do produto para o ID {id}.", ex);
      }
    }

  }
}
