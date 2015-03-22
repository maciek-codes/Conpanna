namespace Conpanna
{
    internal interface IRequestDataProvider
    {
        /// Gets request method
        /// </summary>
        string Method { get; }

        /// <summary>
        /// Gets the original url
        /// </summary>
        string RawUrl { get; }
    }
}
