namespace Conpanna.Tests
{
    using System;
    using Xunit;

    public class MethodHelperTest
    {
        [Fact]
        void MethodHelperConvertsStringToEnum()
        {
            Assert.Equal(Method.GET, MethodHelper.FromString("get"));
            Assert.Equal(Method.GET, MethodHelper.FromString("GET"));
            Assert.Equal(Method.POST, MethodHelper.FromString("POST "));
        }

        [Fact]
        void MethodHelperFailsWhenNotRecognized()
        {
            Assert.Throws(typeof(ArgumentNullException), () => MethodHelper.FromString(null));
            Assert.Throws(typeof(ArgumentException), () => MethodHelper.FromString(""));
            Assert.Throws(typeof(ArgumentException), () => MethodHelper.FromString("ABCDK"));
        }
    }
}
