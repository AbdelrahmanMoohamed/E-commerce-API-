using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Reposatory.Data
{
    public class StoreContextSeed
    {
        //Seeding
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if (!dbcontext.ProductBrands.Any())
            {

                var BrandData = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/brands.json");

                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(Brand);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }


            //Seeding Types
            if (!dbcontext.productTypes.Any())
            {
                var TypeData = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(Type);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }



            //Seeding Products
            if (!dbcontext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await dbcontext.Set<Product>().AddAsync(Product);
                    }
                    await dbcontext.SaveChangesAsync();
                }

            }
        }
    }
}
