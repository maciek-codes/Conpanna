namespace Conpanna.HttpWrapper
{
    using System;
    using System.IO;
    using System.Net;

    internal class HttpListenerResponseWrapper : IResponseDataProvider
    {
        private HttpListenerResponse _response;

        public HttpListenerResponseWrapper(HttpListenerResponse response)
        {
            _response = response;
        }

        public long ContentLength
        {
            get
            {
                return _response.ContentLength64;
            }

            set
            {
                _response.ContentLength64 = value;
            }
        }

        public Stream OutputStream
        {
            get
            {
                return _response.OutputStream;
            }
        }

        public void Close()
        {
            _response.Close();
        }
    }
}
