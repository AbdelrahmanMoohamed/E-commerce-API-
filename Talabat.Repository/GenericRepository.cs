using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Reposatory.Data;

namespace Talabat.Reposatory
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }



        #region Without Specification
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //{
            //    return (IReadOnlyCollection<T>)await _dbContext.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
            //}
            return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        #endregion

        #region Wiith Specification
        public async Task<IReadOnlyList<T>> GetAllWIthSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplaySpecification(Spec).ToListAsync();
        }


        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplaySpecification(Spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplaySpecification(ISpecifications<T> Spec )
        {
            return  SpecificationEvaluator<T>.GetQuerry(_dbContext.Set<T>(), Spec);
        }

        public async Task<int> GetCountForAllProductAsync(ISpecifications<T> Spec)
        {
            return await ApplaySpecification(Spec).CountAsync();
        }
        #endregion
    }
}