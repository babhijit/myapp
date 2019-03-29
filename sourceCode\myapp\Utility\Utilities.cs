using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public static class Utilities
    {
        public static T ConvertValue<T, U>(U value) where U : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
