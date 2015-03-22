namespace Conpanna
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Router
    {
        private Dictionary<string, RouteHandlerCollection> _routes = new Dictionary<string, RouteHandlerCollection>();

        internal void Add(Method method, string routeName, Action<Request, Response> handle)
        {
            if(method == Method.Invalid)
            {
                throw new ArgumentException("Invalid method", "routeName");
            }

            if(string.IsNullOrWhiteSpace(routeName))
            {
                throw new ArgumentException("Route name cannot be empty", "routeName");
            }

            if (_routes.ContainsKey(routeName))
            {
                _routes[routeName].Add(new Route(method, handle));
            }
            else
            {
                _routes.Add(routeName, new RouteHandlerCollection()
                {
                    new Route(method, handle)
                });
            }
        }

        public IEnumerable<Action<Request, Response>> Get(Method method, string routeName)
        {
            if (method == Method.Invalid)
            {
                throw new ArgumentException("Invalid method", "routeName");
            }

            if (string.IsNullOrWhiteSpace(routeName))
            {
                throw new ArgumentException("Route name cannot be empty", "routeName");
            }

            if (!_routes.ContainsKey(routeName))
            {
                return null;
            }

            return from route in _routes[routeName]
                where route.Method == method
                select route.Handler;
        }

        private class RouteHandlerCollection : List<Route>
        {
        }
    }
}
