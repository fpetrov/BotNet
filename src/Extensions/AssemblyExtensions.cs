using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BotNet.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesWithAttribute<T>(this Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(T)));
        }
    }
}
