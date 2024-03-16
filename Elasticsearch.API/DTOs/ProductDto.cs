using DevopsTest.Models;

namespace Elasticsearch.API.DTOs
{

    public record ProductDto(Guid Id, string Name, string Description, decimal Price, int Stock, ProductFeatureDto? Feature)
    {

    }

}
