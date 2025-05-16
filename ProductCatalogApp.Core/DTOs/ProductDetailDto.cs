namespace ProductCatalog.Core.DTOs
{

  public class ProductDetailDto
  {
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public RatingDto? Rating { get; set; }


    public string? RatingDisplay => $"{Rating.Rate} / 5 ({Rating.Count} avaliações)";

  }

  public class RatingDto
  {
    public double Rate { get; set; }
    public int Count { get; set; }
  }
}