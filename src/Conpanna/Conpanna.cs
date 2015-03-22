namespace Conpanna
{
	using System;
	using System.Diagnostics;
	using System.Net;
	using System.Threading;

    using HttpWrapper;
    using SimpleInjector;

    /// <summary>
    /// Conpanna server
    /// </summary>
    public class Conpanna : IDisposable
    {
        private readonly Container _container;
        private IHttpListener _listener;
        private readonly HttpHandler _handler;
        private readonly Thread _httpThread;

        /// <summary>
        /// Router
        /// </summary>
        private readonly Router _router = new Router();

        public bool IsListening { get; private set; }

        /// <summary>
        /// Creates new instance of Conpanna server
        /// </summary>
        public Conpanna()
            : this(new Container())
        {
            _container.RegisterSingle<IHttpListener>(new HttpListenerWrapper());
        }

        internal Conpanna(Container container)
        {
            _container = container;
            _handler = new HttpHandler(this);
            _httpThread = new Thread(new ThreadStart(_handler.HandleTcp));
        }

				/// <summary>
				/// Create route handler for GET request
				/// </summary>
				/// <param name="routeName"></param>
				/// <param name="handle"></param>
        public void Get(string routeName, Action<Request, Response> handle)
        {
            _router.Add(Method.GET, routeName, handle);
        }

        public void Post(string routeName, Action<Request, Response> handle)
        {
            _router.Add(Method.POST, routeName, handle);
        }

        /// <summary>
        /// Create route handler for PUT request
        /// </summary>
        /// <param name="routeName"></param>
        /// <param name="handle"></param>
        public void Put(string routeName, Action<Request, Response> handle)
        {
            _router.Add(Method.PUT, routeName, handle);
        }

        public void Delete(string routeName, Action<Request, Response> handle)
        {
            _router.Add(Method.DELETE, routeName, handle);
        }

				/// <summary>
				/// Start listening
				/// </summary>
				/// <param name="host"></param>
				/// <param name="port"></param>
				/// <param name="ListenCallback"></param>
        public void Listen(string host, int port, Action ListenCallback = null)
        {
						if (!HttpListener.IsSupported)
						{
							Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
							return;
						}

            // Create a listener.
            _listener = _container.GetInstance<IHttpListener>();

            var hostPrefix = CreatePrefix(host, port);

            _listener.Prefixes.Add(hostPrefix);
            IsListening = true;
            _listener.Start();

            _httpThread.Start();
            _httpThread.IsBackground = true;

            if (ListenCallback != null)
            {
                ListenCallback();
            }
        }

        /// <summary>
        /// Close Conpanna server gracefully
        /// </summary>
        public void Close()
        {
            IsListening = false;
            _listener.Close();
            _httpThread.Join();
        }

        /// <summary>
        /// Return URI prefix
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private string CreatePrefix(string host, int port)
				{
						if (!host.StartsWith("https://") && !host.StartsWith("http://"))
						{
							host = "http://" + host;
						}

						host += ":" + port.ToString() + "/";

						return host;
        }

        /// <summary>
        /// Http handler class, represents object processing incoming requests
        /// </summary>
        private class HttpHandler
        {
						private readonly Conpanna _conpanna;

            /// <summary>
            /// Create new handler
            /// </summary>
            /// <param name="conpanna"></param>
						public HttpHandler(Conpanna conpanna)
            {
								_conpanna = conpanna;
            }

            /// <summary>
            /// Handle incoming requests
            /// </summary>
            public void HandleTcp()
            {
								try
								{
										while (_conpanna.IsListening)
										{
												var context = _conpanna._listener.GetContext();
                        var method = MethodHelper.FromString(context.Request.Method);
                        var handlers = _conpanna._router.Get(method, context.Request.OriginalUrl);

                        if(handlers == null)
                        {
                            context.Response.Send("Cannot {0} {1}", context.Request.Method, context.Request.OriginalUrl);
                        }

                        foreach (var handler in handlers)
                        {
                            handler(context.Request, context.Response);
                        }

                        // Now, close the response stream
                        context.Response.Close();
										}
								}
								catch (HttpListenerException ex)
								{
									Debug.WriteLine(ex.Message);
								}
						}
        }

        #region IDisposable

        /// <summary>
        /// Close the listener
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                Close();
            }
        }

        #endregion // IDispsable
    }
}
