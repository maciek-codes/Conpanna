namespace Conpanna.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using SimpleInjector;
    using Moq;
    using Xunit; 

    public class RequestTests
    {
        private readonly Container _container = new Container();
        private readonly MemoryStream _memoryStream = new MemoryStream();

        private readonly Queue<IHttpContext> _requestsList = new Queue<IHttpContext>();

        private Conpanna SetUpMockServer()
        {
            // Pass our container with mock HTTP service
            Conpanna app = null;

            var mockHttpListener = new Mock<IHttpListener>();

            mockHttpListener.Setup(listener => listener.Prefixes).Returns(new List<string>());

            // Return next pending request
            mockHttpListener.Setup(listener => listener.GetContext()).Returns(() =>
            {
                while (_requestsList.Count == 0 && app.IsListening)
                {
                    continue;
                }

                return _requestsList.Dequeue();
            });

            _container.RegisterSingle<IHttpListener>(mockHttpListener.Object);

            app = new Conpanna(_container);

            return app;
        }

        /// <summary>
        /// Check if methos is set correctly
        /// </summary>
        [Fact]
        public async void MethodCorrespondsToRequest()
        {
            var address = "localhost";
            var port = 8080;
            var app = SetUpMockServer();

            int called = 0;

            // Add a router handler for /hello
            app.Get("/hello", (req, res) =>
            {
                // Check if data about the request is propagated
                Assert.Equal("GET", req.Method);
                Assert.Equal("/hello", req.OriginalUrl);
                called++;
            });

            app.Post("/hello", (req, res) =>
            {
                Assert.Equal("POST", req.Method);
                Assert.Equal("/hello", req.OriginalUrl);
                called++;
            });

            // Start our server
            app.Listen(address, port);

            // Not calling before making a request
            Assert.Equal(0, called);

            // Make a request
            SimulateGetRequest("/hello");

            // Give it some time
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            Assert.Equal(1, called);

            // Make a POST request
            SimulatePostRequest("/hello");

            // Give it some time
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            // Should be called now
            Assert.Equal(2, called);

            app.Close();
        }


        /// <summary>
        /// Simulate a GET request
        /// </summary>
        /// <param name="route">Request's route</param>
        /// <param name="method">Request's method</param>
        private void SimulateGetRequest(string route)
        {
            SimulateRequest(route, "GET");
        }

        /// <summary>
        /// Simulate a POST request
        /// </summary>
        /// <param name="route">Request's route</param>
        /// <param name="method">Request's method</param>
        private void SimulatePostRequest(string route)
        {
            SimulateRequest(route, "POST");
        }

        /// <summary>
        /// Simulate a PUT request
        /// </summary>
        /// <param name="route">Request's route</param>
        /// <param name="method">Request's method</param>
        private void SimulatePutRequest(string route)
        {
            SimulateRequest(route, "PUT");
        }

        /// <summary>
        /// Simulate a request
        /// </summary>
        /// <param name="route">Request's route</param>
        /// <param name="method">Request's method</param>
        private void SimulateRequest(string route, string method)
        {
            var mockRequestDataProvider = new Mock<IRequestDataProvider>();
            mockRequestDataProvider.Setup(dp => dp.Method).Returns(method);
            mockRequestDataProvider.Setup(dp => dp.RawUrl).Returns(route);

            var mockResponseDataProvider = new Mock<IResponseDataProvider>();
            mockResponseDataProvider.SetupGet(res => res.OutputStream).Returns(_memoryStream);
            var mockContext = new Mock<IHttpContext>();

            // Mock request
            mockContext.Setup(ctx => ctx.Request).Returns(
                new Request(mockRequestDataProvider.Object));

            // Mock response
            mockContext.Setup(ctx => ctx.Response).Returns(
                new Response(mockResponseDataProvider.Object));

            _requestsList.Enqueue(mockContext.Object);
        }
    }
}
