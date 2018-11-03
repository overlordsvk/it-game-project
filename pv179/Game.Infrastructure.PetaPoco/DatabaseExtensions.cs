using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AsyncPoco;

namespace Game.Infrastructure.PetaPoco
{
    public static class DatabaseExtensions
    {
        public static async Task<object> InvokeSingleOrDefaultAsync(this IDatabase database, Type typeParameter, int? id)
        {
            var genericMethod = typeof(Database)
                .GetMethod(nameof(Database.SingleOrDefaultAsync), BindingFlags.Public | BindingFlags.Instance, null,
                    new[] { typeof(object) }, null)
                ?.MakeGenericMethod(typeParameter);
            dynamic awaitable = genericMethod?.Invoke(database, new[] { (object)id }) ??
                                throw new InvalidOperationException("No such method exists!");
            await awaitable;
            object value = awaitable.GetAwaiter().GetResult();
            return value;
        }
    }
}
