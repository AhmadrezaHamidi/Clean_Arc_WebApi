﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AhFramWork.Application.Conteracts.Common
{
    public class CatalogActionResult<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int Total { get; set; } = 0;
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
    public class GridQueryDto
    {
        public int Size { get; set; } = 10;
        public int Page { get; set; } = 1;
        public SortModel[] Sorted { get; set; } = new SortModel[]
        {
            new SortModel
            {
                column = "Id",
                desc = true
            }
        };
        public FilterModel[] Filtered { get; set; } = new FilterModel[] { };
    }

    public class FilterModel
    {
        public string column { get; set; }
        public string value { get; set; }
    }

    public class SortModel
    {
        public string column { get; set; }
        public bool desc { get; set; } = false;
    }
    public static class QueryUtility
    {
        public static IOrderedQueryable<TEntityType> SortMeDynamically<TEntityType>(this IQueryable<TEntityType> query, string propertyname, bool desc = false)
        {
            propertyname = propertyname ?? "Id";

            propertyname = typeof(TEntityType).GetPropertyExactName(propertyname);

            var param = Expression.Parameter(typeof(TEntityType), "s");
            var prop = Expression.PropertyOrField(param, propertyname);
            var sortLambda = Expression.Lambda(prop, param);

            Expression<Func<IOrderedQueryable<TEntityType>>> sortMethod;

            if (desc)
                sortMethod = (() => query.OrderByDescending<TEntityType, object>(k => null));
            else
                sortMethod = (() => query.OrderBy<TEntityType, object>(k => null));

            var methodCallExpression = (sortMethod.Body as MethodCallExpression);
            if (methodCallExpression == null)
                throw new Exception("Oops");

            var method = methodCallExpression.Method.GetGenericMethodDefinition();
            var genericSortMethod = method.MakeGenericMethod(typeof(TEntityType), prop.Type);
            var orderedQuery = (IOrderedQueryable<TEntityType>)genericSortMethod.Invoke(query, new object[] { query, sortLambda });

            return orderedQuery;
        }

        public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> query, string by, bool desc = true) where T : class
        {
            by = by ?? "Id";

            by = typeof(T).GetPropertyExactName(by);

            //return desc
            //    ? query.OrderByDescending(o => o.GetType().GetProperty(by).GetValue(o))
            //    : query.OrderBy(o => o.GetType().GetProperty(by).GetValue(o));

            if (desc)
                return query.OrderByDescending(o => o.GetType().GetProperty(by).GetValue(o));
            return query.OrderBy(o => o.GetType().GetProperty(by).GetValue(o));
        }


        public static Expression<Func<T, bool>> FilterExpression<T>(string propertyName, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyName))
                return null;
            try
            {
                propertyName = typeof(T).GetPropertyExactName(propertyName);
                var propertyType = typeof(T).GetPropertyType(propertyName);

                object castedPropertyValue = new object();
                string @operator = string.Empty;
                if (propertyType == typeof(bool) || propertyType == typeof(Nullable<bool>))
                {
                    @operator = "Equals";
                    castedPropertyValue = bool.Parse(propertyValue);
                }
                if (propertyType == typeof(int) || propertyType == typeof(Nullable<int>))
                {
                    @operator = "Equals";
                    castedPropertyValue = int.Parse(propertyValue);
                }
                if (propertyType == typeof(string))
                {
                    @operator = "Contains";
                    castedPropertyValue = propertyValue;
                }



                var parameterExp = Expression.Parameter(typeof(T), "type");
                var propertyExp = Expression.Property(parameterExp, propertyName);


                var someValue = Expression.Constant(castedPropertyValue, propertyType);

                if (propertyType == typeof(Nullable<int>))
                {

                    MethodInfo methodbool = propertyType.GetMethod(@operator, new[] { propertyType });

                    var convertedSomeValue = Expression.Convert(someValue, typeof(object));

                    var containsMethodExp = Expression.Call(propertyExp, methodbool, convertedSomeValue);
                    return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);

                }
                if (propertyType == typeof(Nullable<bool>))
                {

                    MethodInfo methodbool = propertyType.GetMethod(@operator, new[] { propertyType });

                    var convertedSomeValue = Expression.Convert(someValue, typeof(object));

                    var containsMethodExp = Expression.Call(propertyExp, methodbool, convertedSomeValue);
                    return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);

                }
                else
                {
                    MethodInfo method = propertyType.GetMethod(@operator, new[] { propertyType });

                    var containsMethodExp = Expression.Call(propertyExp, method, someValue);
                    return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);

                }


            }
            catch (Exception ex)
            {

                return null;
            }

        }


        public static Expression<Func<T, T>> SelectExpression<T>(string propertyName, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyName))
                return null;
            try
            {
                propertyName = typeof(T).GetPropertyExactName(propertyName);
                var propertyType = typeof(T).GetPropertyType(propertyName);

                var parameterExp = Expression.Parameter(typeof(T), "type");
                var propertyExp = Expression.Property(parameterExp, propertyName);


                // var someValue = Expression.Constant(castedPropertyValue, propertyType);
                //var containsMethodExp = Expression.Call(propertyExp, method, someValue);

                return Expression.Lambda<Func<T, T>>(null, parameterExp);
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        static string GetPropertyExactName(this Type type, string name)
        {
            return type.GetProperties()
                 .Where(x => x.Name.ToLower() == name.ToLower())
                 .Select(x => x.Name)
                 .FirstOrDefault();
        }

        static Type GetPropertyType(this Type type, string name)
        {
            return type.GetProperties()
                 .Where(x => x.Name.ToLower() == name.ToLower())
                 .Select(x => x.PropertyType)
                 .FirstOrDefault();
        }

        public static IQueryable<T> SelectDynamic<T>(this IQueryable<T> source, IEnumerable<string> fields)
        {
            var fieldNames = new List<string>();
            foreach (var field in fields)
            {
                fieldNames.Add(GetPropertyExactName(typeof(T), field));
            }

            ParameterExpression expression = Expression.Parameter(typeof(T), "s");
            MemberBinding[] bindings = new MemberBinding[fields.Count()];

            for (int i = 0; i < fields.Count(); i++)
            {
                var name = fieldNames.ElementAt(i);
                var binding = Expression.Bind(typeof(T).GetProperty(name), Expression.PropertyOrField(expression, name));
                bindings[i] = binding;
            }

            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            var lambda = Expression.Lambda<Func<T, T>>(Expression.MemberInit(Expression.New(typeof(T)), bindings), parameters);

            return source.Select(lambda);
        }


    }
}

