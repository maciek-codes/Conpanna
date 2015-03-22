namespace Conpanna
{
    using System;

    internal class Route
    {
        public Route(Method method, Action<Request, Response> handler)
        {
            Method = method;
            Handler = handler;
        }

        public Method Method { get; private set; }

        public Action<Request, Response> Handler { get; private set; }
    }
}
