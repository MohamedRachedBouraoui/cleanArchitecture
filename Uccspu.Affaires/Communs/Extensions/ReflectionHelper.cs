using System;
using System.Linq.Expressions;

namespace Uccspu.Affaires.Communs.Extensions
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Permet d'avoir le nom d'Une propriété en évitant l'tuilisation des 'Magic strings'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string PropertyName<T, P>(Expression<Func<T, P>> property)
            where T : class
        {
            MemberExpression body = (MemberExpression)property.Body;
            return body.Member.Name;
        }
    }
}
