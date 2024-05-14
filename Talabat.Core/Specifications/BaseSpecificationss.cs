using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecificationss<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Carteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set ; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagenationEnabled { get; set; }

        //Get All
        public BaseSpecificationss()
        {
            //Includes = new List<Expression<Func<T, object>>>();
        }

        // Get By ID
        public BaseSpecificationss(Expression<Func<T,bool>> carteria)
        {
            Carteria = carteria;
        }

        //Oreder By: OrederBy(P=>P.Value)
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        //Oreder By Decinding: OrederByDesc(P=>P.Value)
        public void AddOrderByDecinding(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        public void ApllayPagenation(int skip , int take)
        {
            IsPagenationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
