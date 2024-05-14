using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Reposatory
{
    public interface IBasketRepo
    {
        //Get Basket
        Task<CustomerBasket?> GetBasketAsync(string BasketId);

        //Update & Create Basket
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);

        Task<bool> DeleteBasketAsync( string BasketId);

    }
}
