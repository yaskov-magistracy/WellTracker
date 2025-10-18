using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;

namespace DAL.EfCoreExtensions;

public static class EntityTypeBuilderExtensions
{
    public static IndexBuilder<TEntity> HasFullTextSearchIndex<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity,NpgsqlTsVector>> tsVectorPropertyExpression, 
        Expression<Func<TEntity,object>> includeExpression,
        string config = "russian"
        ) where TEntity : class
    {
        return builder
            .HasGeneratedTsVectorColumn(
                tsVectorPropertyExpression,
                config,
                includeExpression) 
            .HasIndex(tsVectorPropertyExpression.ToObjectExpression())
            .HasMethod("GIN");
    }
    
    public static Expression<Func<TEntity, object?>> ToObjectExpression<TEntity>(
        this Expression<Func<TEntity, NpgsqlTsVector>> tsVectorExpression)
    {
        var parameter = tsVectorExpression.Parameters[0];
        var convertExpression = Expression.Convert(tsVectorExpression.Body, typeof(object));
        
        return Expression.Lambda<Func<TEntity, object?>>(convertExpression, parameter);
    }
}