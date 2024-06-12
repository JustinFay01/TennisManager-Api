using AutoMapper;

namespace tennismanager_api.tennismanager.shared.Extensions;

public static class MappingExpressionExtensions
{
    /// <summary>
    /// Must be called first as this extension uses ForAllMembers
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDest"></typeparam>
    /// <returns></returns>
    public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
    {
        expression.ForAllMembers(opt => opt.Ignore());
        return expression;
    }
}