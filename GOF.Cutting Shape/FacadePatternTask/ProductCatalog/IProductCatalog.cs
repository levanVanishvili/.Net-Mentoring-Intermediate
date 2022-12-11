using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePatternTask.Product
{
    public interface IProductCatalog
    {
        Product GetProductDetails(string productId);
    }
}
