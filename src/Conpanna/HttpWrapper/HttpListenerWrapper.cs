namespace Conpanna.HttpWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    internal class HttpListenerWrapper : IHttpListener, IDisposable
    {
        private readonly HttpListener _httpListener;

        public HttpListenerWrapper()
        {
            _httpListener = new HttpListener();
        }

        public ICollection<string> Prefixes
        {
            get
            {
                return _httpListener.Prefixes;
            }
        }

        public void Close()
        {
            if (_httpListener != null)
            {
                _httpListener.Close();
            }
        }

        public void Dispose()
        {
            if (_httpListener != null)
            {
                _httpListener.Close();
            }
        }

        public IHttpContext GetContext()
        {
           return new HttpContextWrapper(_httpListener.GetContext());
        }

        public void Start()
        {
            _httpListener.Start();
        }

        public void Stop()
        {
            _httpListener.Stop();
        }
    }
}
