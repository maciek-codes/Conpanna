namespace Conpanna.HttpWrapper
{
    using System.Net;

    internal class HttpListenerRequestWrapper : IRequestDataProvider
    {
        private readonly HttpListenerRequest _request;

        public HttpListenerRequestWrapper(HttpListenerRequest request)
        {
            _request = request;
        }

        /// <summary>
        /// Get HTTP Method
        /// </summary>
        public string Method
        {
            get
            {
                return _request.HttpMethod;
            }
        }

        public string RawUrl
        {
            get
            {
                return _request.RawUrl;
            }
        }
    }
}
