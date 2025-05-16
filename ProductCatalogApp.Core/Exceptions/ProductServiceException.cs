namespace ProductCatalog.Core.Exceptions
{
  public class ProductServiceException : Exception
  {
    public ProductServiceException(string message) : base(message) { }

    public ProductServiceException(string message, Exception innerException)
        : base(message, innerException) { }
  }
}
