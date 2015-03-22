namespace Conpanna
{
    using System;

    internal enum Method
    {
        GET,
        DELETE,
        POST,
        PUT
    }

    internal static class MethodHelper
    {
        /// <summary>
        /// Convert string to <see cref="Method"/> value
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Method FromString(string method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Method cannot be empty", "method");
            }

            return (Method)Enum.Parse(typeof(Method), method, true);
        }
    }
}
