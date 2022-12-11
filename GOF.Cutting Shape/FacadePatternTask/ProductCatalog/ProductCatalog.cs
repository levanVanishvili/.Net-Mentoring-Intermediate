using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePatternTask.Product
{
    public class ProductCatalog : IProductCatalog
    {
        private IEnumerable<Product>? _products = Array.Empty<Product>();
        public Product GetProductDetails(string productId)
        {
            Console.WriteLine("Get Product Details");
            return _products.FirstOrDefault(x => x.Id == productId);
        }
    }

    
}
