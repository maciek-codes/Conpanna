namespace Conpanna
{
    using System.IO;

    /// <summary>
    /// Interface for objects providing methods and data to response object
    /// </summary>
    internal interface IResponseDataProvider
    {
        /// <summary>
        /// Response content length
        /// </summary>
        long ContentLength { get; set; }

        /// <summary>
        /// Response output stream
        /// </summary>
        Stream OutputStream { get; }

        /// <summary>
        /// Close the response and its stream
        /// </summary>
        void Close();
    }
}
