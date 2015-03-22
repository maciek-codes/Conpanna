namespace Conpanna
{
    using System;

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

            Method result;

            // Try to parse input with ignoring case
            if (Enum.TryParse(method, true, out result))
            {
                return result;
            }

            return Method.Invalid;
        }
    }

}
