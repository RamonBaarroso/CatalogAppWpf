using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Windows;
using Polly;
using Polly.Extensions.Http;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Core.Services;
using ProductCatalog.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace ProductCatalogApp
{
  public partial class App : Application
  {
    public static IServiceProvider? ServiceProvider { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var configuration = new ConfigurationBuilder()
     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
     .Build();


      var apiBaseUrl = configuration["ApiSettings:BaseUrl"];

      var services = new ServiceCollection();

      services.AddHttpClient<IProductService, ProductService>(client =>
      {
        client.BaseAddress = new Uri(apiBaseUrl);
        client.Timeout = TimeSpan.FromSeconds(10);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
      });

      services.AddSingleton<IJsonCacheService, JsonCacheService>();
      services.AddSingleton<IConfiguration>(configuration);

      var serviceProvider = services.BuildServiceProvider();

      var mainWindow = ActivatorUtilities.CreateInstance<MainWindow>(serviceProvider);
      mainWindow.Show();
    }

  }
}
