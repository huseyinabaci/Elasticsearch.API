using Nest;

namespace DevopsTest.Models
{
    public class Product
    {
        [PropertyName("_id")]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set;}
        public ProductFeature? Feature { get; set; }

        public ProductDto CreateDto()
        {
            if (Feature == null)
                return 
        }



    }
}
