using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR6;

namespace UnitTestProject
{
	[TestClass]
	public class UnitTest2
	{
		[TestMethod]
		public void AuthTest()
		{
			var page = new AuthPage();
			Assert.IsTrue(page.Auth("test", "test"));
            Assert.IsFalse(page.Auth("user1", "12345"));
            Assert.IsFalse(page.Auth("", ""));
            Assert.IsFalse(page.Auth(" ", " "));
        }
	}
}
