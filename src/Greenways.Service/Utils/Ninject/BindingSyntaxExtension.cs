using System;
using System.Linq.Expressions;
using Ninject.Syntax;

namespace Greenways.Utils.Ninject
{
    public static class BindingSyntaxExtension
    {
        public static IBindingWithOrOnSyntax<T> WithPropertyValue<T, TProperty>(this IBindingWithSyntax<T> syntax, Expression<Func<T, TProperty>> expression, TProperty value)
        {
            return WithPropertyValue((IBindingWithOrOnSyntax<T>)syntax, expression, value);
        }

        public static IBindingWithOrOnSyntax<T> WithPropertyValue<T, TProperty>(this IBindingWithOrOnSyntax<T> syntax, Expression<Func<T, TProperty>> expression, TProperty value)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var propertyName = memberExpression.Member.Name;
            syntax.WithPropertyValue(propertyName, value);
            return syntax;
        }
    }
}