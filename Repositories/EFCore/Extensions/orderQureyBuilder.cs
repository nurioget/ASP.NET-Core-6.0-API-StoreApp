using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Extensions
{
    public static class OrderQureyBuilder
    {
        public static String? CreateOrderQurey<T>(String orderByQueryString)
        {
            var orderParams = orderByQueryString.Trim().Split(",");

            var propertyInfos = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            //title asceding, price descending, id ascending[,]
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];

                var objectProprty = propertyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName,
                    StringComparison.InvariantCultureIgnoreCase));

                if (objectProprty is null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProprty.Name.ToString()} {direction},");

            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery;
        }
    }
}
