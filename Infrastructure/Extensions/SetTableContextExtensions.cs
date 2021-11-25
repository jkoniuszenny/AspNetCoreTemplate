using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class SetTableContextExtensions
    {
        public static IQueryable<object> Set(this DbContext _context, Type t)
        {
            return (IQueryable<object>)_context.GetType()
            .GetMethods()
            .First(p => p.Name == "Set" && p.ContainsGenericParameters)
            .MakeGenericMethod(t)
            .Invoke(_context, null);
        }
    }
}
