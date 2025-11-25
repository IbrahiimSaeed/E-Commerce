using Domain.Entites.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductSpecificationParameters parameters) :
            base(p => (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) &&
                        (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId) &&
                        (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {
            
        }
    }
}
