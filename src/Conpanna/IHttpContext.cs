namespace Conpanna
{
    internal interface IHttpContext
    {
        Request Request { get; }

        Response Response { get; }
    }
}
