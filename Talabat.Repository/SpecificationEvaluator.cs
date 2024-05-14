using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Reposatory
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuerry(IQueryable<T> InputQuery, ISpecifications<T> Spec)
        {
            #region To Butild The Collection

            var Query = InputQuery; //_dbContext.Set < T > 
            #endregion

            #region To Build Where Expression

            if (Spec.Carteria != null) //P => P.id = id
            {
                Query = Query.Where(Spec.Carteria);//_dbContext.Set<T>.Where(P => P.id=id)
            }

            #endregion

            #region To Build OrderBy || Order By Descending
            if (Spec.OrderBy != null)
            {
                Query = Query.OrderBy(Spec.OrderBy);
            }
            if (Spec.OrderByDesc != null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDesc);
            }
            #endregion

            #region Build The Skip And Take For Pagination
            if (Spec.IsPagenationEnabled)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }
            #endregion

            #region To Build Include

            //_dbContext.Set<T>.Where(P => P.id=id).Include(P=>P.ProductBrand)
            //_dbContext.Set<T>.Where(P => P.id=id).Include(P=>P.ProductBrand).Include(P=>P.ProductType)
            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            #endregion

            return Query;
        }
    }
}
