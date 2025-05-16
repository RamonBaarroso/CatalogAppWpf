namespace ProductCatalog.Core.DTOs
{

  public class ProductListDto
  {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
  }
}
