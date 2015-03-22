namespace Conpanna.Tests
{
    using Moq;
    using System.IO;
    using Xunit;
    using System;
    using System.Text;

    public class ResponseTests
    {
        [Fact]
        public void SendWritesToOutputStream()
        {
            using (var ms = new MemoryStream())
            {
                var mockResponseDataProvider = new Mock<IResponseDataProvider>();
                mockResponseDataProvider.Setup(r => r.OutputStream).Returns(ms);
                var response = new Response(mockResponseDataProvider.Object);

                // Check normal send
                response.Send("Abc");

                CheckIfInStream("Abc", ms);
            }
        }

        [Fact]
        public void SendWritesToOutputStreamWithParameters()
        {
            using (var ms = new MemoryStream())
            {
                var mockResponseDataProvider = new Mock<IResponseDataProvider>();
                mockResponseDataProvider.Setup(r => r.OutputStream).Returns(ms);
                var response = new Response(mockResponseDataProvider.Object);

                // Check normal send
                response.Send("Abc {0}", "abC");

                var expectedOutput = string.Format("Abc {0}", "abC");
                CheckIfInStream(expectedOutput, ms);
            }
        }

        [Fact]
        public void CloseAlsoClosesTheStream()
        {
            using (var ms = new MemoryStream())
            {
                var mockResponseDataProvider = new Mock<IResponseDataProvider>();
                mockResponseDataProvider.Setup(r => r.OutputStream).Returns(ms);
                var response = new Response(mockResponseDataProvider.Object);

                Assert.True(ms.CanRead);
                Assert.True(ms.CanWrite);

                response.Close();

                Assert.False(ms.CanRead);
                Assert.False(ms.CanWrite);
            }
        }

        private void CheckIfInStream(string expectedContent, Stream stream)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(expectedContent);
            var actualBytes = new byte[expectedBytes.Length];

            stream.Position = 0;
            stream.Read(actualBytes, 0, expectedBytes.Length);

            Assert.Equal(expectedBytes.Length, actualBytes.Length);
            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
