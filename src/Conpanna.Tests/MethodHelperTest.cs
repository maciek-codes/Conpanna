namespace Conpanna.Tests
{
    using System;
    using Xunit;

    public class MethodHelperTest
    {
        [Fact]
        void MethodHelperConvertsStringToEnum()
        {
            Assert.Equal(Method.Get, MethodHelper.FromString("get"));
            Assert.Equal(Method.Get, MethodHelper.FromString("GET"));
            Assert.Equal(Method.Post, MethodHelper.FromString("POST "));
            Assert.Equal(Method.Invalid, MethodHelper.FromString("abc"));
        }

        [Fact]
        void MethodHelperFailsWhenNotRecognized()
        {
            Assert.Throws(typeof(ArgumentNullException), () => MethodHelper.FromString(null));
            Assert.Throws(typeof(ArgumentException), () => MethodHelper.FromString(""));
        }
    }
}
