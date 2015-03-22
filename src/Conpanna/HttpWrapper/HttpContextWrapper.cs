namespace Conpanna.HttpWrapper
{
    using System.Net;

    internal class HttpContextWrapper : IHttpContext
    {      
        public HttpContextWrapper(HttpListenerContext context)
        {
            Request = new Request(new HttpListenerRequestWrapper(context.Request));
            Response = new Response(new HttpListenerResponseWrapper(context.Response));
        }

        public Request Request { get; private set; }

        public Response Response { get; private set; }
    }
}
