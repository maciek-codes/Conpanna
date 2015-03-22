namespace Conpanna
{
    using System.Collections.Generic;

    internal interface IHttpListener
    {
        ICollection<string> Prefixes { get; }

        void Start();

        void Stop();

        IHttpContext GetContext();
        void Close();
    }
}
