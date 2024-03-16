namespace Elasticsearch.API.DTOs
{
    public class ProductFeatureDto
    {
        public record ProductFeatureDto(int Width, int Height, string Color)
        {
        }
    }
}
