using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        // For where 
        public Expression<Func<T,bool>> Carteria { get; set; }

        //For Include
        public List<Expression<Func<T,object>>> Includes { get; set; }

        //For Oreder By: OrederBy(P=>P.Value)
        public Expression<Func<T,object>> OrderBy { get; set; }

        //For Order By Desc : OrederByDesc(P=>P.Value)
        public Expression<Func<T, object>> OrderByDesc { get; set; }

        //Skip
        public int Skip { get; set; }

        //Take
        public int Take { get; set; }

        public bool IsPagenationEnabled { get; set; }

    }
}
