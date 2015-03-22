using System;

namespace Conpanna
{
    /// <summary>
    /// Response object
    /// </summary>
    public class Response
    {
        private readonly IResponseDataProvider _response;

        /// <summary>
        /// Wraps an instance of <see cref="HttpListenerResponse"/>
        /// </summary>
        internal Response(IResponseDataProvider response)
        {
            _response = response;
        }

        /// <summary>
        /// Send HTTP response
        /// </summary>
        /// <param name="body">Response body</param>
        public void Send(string body)
        {
            Send(body, null);
        }

        public void Send(string format, params object[] args)
        {
            // Check if format parameters were passed
            if (args != null)
            {
                format = string.Format(format, args);
            }

            // Response as byte array
            var buffer = System.Text.Encoding.UTF8.GetBytes(format);

            // Get a response stream and write the response to it.
            _response.ContentLength = buffer.Length;
            _response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Closes response output stream
        /// </summary>
        internal void Close()
        {
            _response.OutputStream.Close();
            _response.Close();
        }
    }
}
