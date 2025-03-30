using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR6;

namespace UnitTestProject
{
	[TestClass]
	public class RegTest
	{
        [TestMethod]
        public void SuccessfulRegistration()
        {
            var page = new RegPage();
            bool result = page.Register(
                "Киреева Анфиса Анатольевна", "anfisa05@mail.com",
                "password123", "password123",
                "Клиент", "+79991234567",
                new DateTime(1990, 1, 1), "Москва");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MissingRequiredFields()
        {
            var page = new RegPage();
            bool result = page.Register(
                "", "test@mail.com",  
                "password123", "password123",
                "Клиент", "+79991234567",
                null, "");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PasswordMismatch()
        {
            var page = new RegPage();
            bool result = page.Register(
                "Петров Петр Петрович", "test2@mail.com",
                "password123", "123",
                "Клиент", "+79991234567",
                null, "Москва");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExistingEmail()
        {
            var page = new RegPage();

            bool result = page.Register(
                "Мозгунова Дарья Алексеевна", "darya.mozgunova@gmail.com", 
                "pass456", "pass456",
                "Хореограф", "+79994445566",
                null, "Москва");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EmptyFields()
        {
            var page = new RegPage();
            bool result = page.Register(
                "", "", "", "", "", "", null, "");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InvalidEmailFormat()
        {
            var page = new RegPage();
            bool result = page.Register(
                "Борисова Мария Генадьевна", "mar07gmail.com",
                "pass123", "pass123",
                "Хореограф", "+79998887766",
                null, "Москва");

            Assert.IsFalse(result);
        }
    }
}
