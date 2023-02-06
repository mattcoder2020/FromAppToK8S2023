using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Common.DataAccess
{
    
    public abstract class BaseSpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public Expression<Func<T, object>> Orderby { get; private set; }
        public Expression<Func<T, bool>> AscOrdered { get; private set; }
        public List<Expression<Func<T, object>>> IncludeList { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void AddIncludes(Expression<Func<T, object>> includeItem )
        {
            IncludeList.Add(includeItem);
        }

        public void AddSort(Expression<Func<T, object>> sortItem)
        {
            Orderby = sortItem;
        }

        public IQueryable<T> GenerateIQuerable (IQueryable<T> queryable)
        {
            
            if (Criteria != null)
                queryable = queryable.Where(Criteria);
            if (Orderby != null)
                queryable = queryable.OrderBy(Orderby);
            if (AscOrdered != null)
                queryable = queryable.OrderByDescending(AscOrdered);

            queryable = IncludeList.Aggregate(queryable, (current, includeitem) => (current.Include(includeitem)));

            return queryable;


        }
     }

    
}