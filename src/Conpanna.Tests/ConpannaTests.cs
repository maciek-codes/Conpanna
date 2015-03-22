namespace Conpanna.Tests
{
    using System.Collections.Generic;
    using SimpleInjector;
    using Moq;
    using Xunit;

    public class ConpannaTests
    {
        private readonly Container _container = new Container();

        public ConpannaTests()
        {
            var mockHttpListener = new Mock<IHttpListener>();
            mockHttpListener.Setup(l => l.Prefixes).Returns(new List<string>());
            _container.RegisterSingle(mockHttpListener.Object);
        }

        [Fact]
        public void CheckThatCallbackFires()
        {
            var address = "localhost";
            var port = 8080;

            // Pass our container with mock HTTP service
            var app = new Conpanna(_container);

            bool called = false;

            app.Listen(address, port, () =>
            {
                called = true;
                return;
            });
    
            // Check if callback was called
            Assert.True(called);

            app.Close();
        }

        /// <summary>
        /// Check that server starts and closes when optional callback not provided
        /// </summary>
        [Fact]
        public void CheckThatStartsFineWithoutCallback()
        {
            var address = "localhost";
            var port = 8080;

            // Pass our container with mock HTTP service
            var app = new Conpanna(_container);

            app.Listen(address, port);
            app.Close();
        }

        /// <summary>
        /// Should listen after Start() and finish after Close();
        /// </summary>
        [Fact]
        public void IsListeningSetByStartAndClose()
        {
            var app = new Conpanna(_container);
            Assert.False(app.IsListening);

            app.Listen("localhost", 1000);
            Assert.True(app.IsListening);

            app.Close();
            Assert.False(app.IsListening);
        }
    }
}
