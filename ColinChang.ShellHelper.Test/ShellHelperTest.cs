using System;
using Xunit;

namespace ColinChang.ShellHelper.Test
{
    public class ShellHelperTest
    {
        [Fact]
        public void ExecuteTest()
        {
            Assert.True(ShellHelper.Execute("dir"));
        }
    }
}
