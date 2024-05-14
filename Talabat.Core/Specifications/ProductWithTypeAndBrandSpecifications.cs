using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecificationss<Product>
    {
        public ProductWithTypeAndBrandSpecifications(ProductSpecParams Params)
            : base(P =>
            (string.IsNullOrEmpty(Params.Search) || P.Name.ToLower(). Contains(Params.Search))
            &&
            (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
            )
            

        {
            //Get All Product With Type And Brand
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDecinding(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            //Products = 100 
            //PageSize = 10 
            //PageIndex = 5 
            //Skip = PageSize *(PageIndex-1)
            //Take = PageSize
            ApllayPagenation(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);
        }

        public ProductWithTypeAndBrandSpecifications(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);

        }
    }
}
