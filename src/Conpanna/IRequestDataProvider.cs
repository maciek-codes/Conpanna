namespace Conpanna
{
    internal interface IRequestDataProvider
    {
        /// <summary>
        /// Gets request method
        /// </summary>
        string Method { get; }

        /// <summary>
        /// Gets the original url
        /// </summary>
        string RawUrl { get; }
    }
}
