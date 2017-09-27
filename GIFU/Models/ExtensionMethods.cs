using System;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static object NullToDBNullValue(this object originalObject)
        {
            return originalObject ?? DBNull.Value;
        }
    }
}