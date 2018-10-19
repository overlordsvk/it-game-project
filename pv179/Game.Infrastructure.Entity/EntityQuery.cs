using Game.DAL.Entity.Entities;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure.UnitOfWork;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Game.Infrastructure.Entity
{
    public class EntityQuery<TEntity> : QueryBase<TEntity> where TEntity : class, IEntity, new()
    {
        private const string LamdaParameterName = "param";

        private readonly ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), LamdaParameterName);

        protected DbContext Context => ((EntityUnitOfWork)Provider.GetUnitOfWorkInstance()).Context;

        /// <summary>
        ///   Initializes a new instance of the <see cref="EntityQuery{TResult}" /> class.
        /// </summary>
        public EntityQuery(IUnitOfWorkProvider provider) : base(provider) { }

        public override async Task<QueryResult<TEntity>> ExecuteAsync()
        {
            throw new NotImplementedException();
            //TODO
        }

        private IQueryable<TEntity> UseSortCriteria(IQueryable<TEntity> queryable)
        {
            var prop = typeof(TEntity).GetProperty(SortAccordingTo);
            var param = Expression.Parameter(typeof(TEntity), "i");
            var expr = Expression.Lambda(Expression.Property(param, prop), param);
            return (IQueryable<TEntity>)typeof(EntityQuery<TEntity>)
                .GetMethod(nameof(UseSortCriteriaCore), BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(prop.PropertyType)
                .Invoke(this, new object[] { expr, queryable });
        }

        private IQueryable<TEntity> UseSortCriteriaCore<TKey>(Expression<Func<TEntity, TKey>> sortExpression,
            IQueryable<TEntity> queryable)
        {
            return UseAscendingOrder ? queryable.OrderBy(sortExpression) : queryable.OrderByDescending(sortExpression);
        }

        private IQueryable<TEntity> UseFilterCriteria(IQueryable<TEntity> queryable)
        {
            throw new NotImplementedException();
            //TODO
        }


        private Expression CombineBinaryExpressions(CompositePredicate compositePredicate)
        {
            if (compositePredicate.Predicates.Count == 0)
            {
                throw new InvalidOperationException("At least one simple predicate must be given");
            }
            var expression = compositePredicate.Predicates.First() is CompositePredicate composite
                ? CombineBinaryExpressions(composite)
                : BuildBinaryExpression(compositePredicate.Predicates.First());
            for (var i = 1; i < compositePredicate.Predicates.Count; i++)
            {
                if (compositePredicate.Predicates[i] is CompositePredicate predicate)
                {
                    expression = compositePredicate.Operator == LogicalOperator.OR ?
                        Expression.OrElse(expression, CombineBinaryExpressions(predicate)) :
                        Expression.AndAlso(expression, CombineBinaryExpressions(predicate));
                }
                else
                {
                    expression = compositePredicate.Operator == LogicalOperator.OR ?
                        Expression.OrElse(expression, BuildBinaryExpression(compositePredicate.Predicates[i])) :
                        Expression.AndAlso(expression, BuildBinaryExpression(compositePredicate.Predicates[i]));
                }
            }
            return expression;
        }

        private Expression BuildBinaryExpression(IPredicate predicate)
        {
            throw new NotImplementedException();
            //TODO
        }
    }
}
