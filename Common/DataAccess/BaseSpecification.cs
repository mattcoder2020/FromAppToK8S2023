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
        public List<Expression<Func<T, object>>> IncludeList { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderbyDesc { get; private set; }

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
        public void AddSortDesc(Expression<Func<T, object>> sortItem)
        {
            OrderbyDesc = sortItem;
        }

        public IQueryable<T> GenerateIQuerable (IQueryable<T> queryable)
        {
            
            if (Criteria != null)
                queryable = queryable.Where(Criteria);
            if (Orderby != null)
                queryable = queryable.OrderBy(Orderby);
            if (OrderbyDesc != null)
                queryable = queryable.OrderByDescending(OrderbyDesc);

            queryable = IncludeList.Aggregate(queryable, (current, includeitem) => (current.Include(includeitem)));

            return queryable;


        }

        
    }

    
}