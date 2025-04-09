using Shipping.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class Specification<T> : ISpecification<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }

        public Specification()
        {

        }

        public Specification(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }
        public void AddCriteria(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        public void AddSorting(string sortBy, bool isSortAscending)
        {
            if (isSortAscending)
            {
                AddOrderBy(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
            }
            else
            {
                AddOrderByDesc(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
            }
        }

        public void AddPaging(int pageNumber, int pageSize)
        {
            Skip = (pageNumber - 1) * pageSize;
            Take = pageSize;
        }

    }
}
