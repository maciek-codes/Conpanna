namespace Conpanna
{
    /// <summary>
    /// Request object
    /// </summary>
	public class Request
    {
        /// <summary>
        /// Object providing data to the request
        /// </summary>
		private readonly IRequestDataProvider _requestData;

        /// <summary>
        /// Create a request object given data provider
        /// </summary>
        internal Request(IRequestDataProvider requestData)
		{
			_requestData = requestData;
        }

		/// <summary>
		/// Gets request method
		/// </summary>
		public string Method
		{
			get
			{
				return _requestData.Method;
			}
		}

        /// <summary>
        /// Gets the original url
        /// </summary>
        public string OriginalUrl
        {
            get
            {
                return _requestData.RawUrl;
            }
        }
    }
}