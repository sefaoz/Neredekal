using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Common.Domain.SeedWorks
{
    public static class Guard
    {
        public static void CannotNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{parameterName} can not be 'null'.");
            }
        }
    }
}
