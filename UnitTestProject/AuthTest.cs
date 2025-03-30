using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR6;

namespace UnitTestProject
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        public void AuthTestSuccess()
        {
            var page = new AuthPage();

            Assert.IsTrue(page.Auth("darya.mozgunova@gmail.com", "Choreo12345"));
            Assert.IsTrue(page.Auth("andrey.mikhaylenko@gmail.com", "Mikhaylenko456"));
            Assert.IsTrue(page.Auth("maria.urlova@gmail.com", "UrlovaMaria789"));

            Assert.IsTrue(page.Auth("vasilisa.kaganovskaya@gmail.com", "VasilisaK2023"));
            Assert.IsTrue(page.Auth("sofya.smirnova@gmail.com", "SmirnovaS4567"));

            Assert.IsTrue(page.Auth("danil.vybornov@gmail.com", "DanilVyb2023"));
            Assert.IsTrue(page.Auth("amir.sidakov@gmail.com", "AmirSid456789"));
            Assert.IsTrue(page.Auth("ali.sidakov@gmail.com", "AliSidakov789"));
            Assert.IsTrue(page.Auth("tatyana.aksenova@gmail.com", "TanyaAks2023"));
            Assert.IsTrue(page.Auth("pavel.andreev@gmail.com", "PavelAndr3456"));
            Assert.IsTrue(page.Auth("marina.nekrasova@gmail.com", "MarinaNek6789"));
            Assert.IsTrue(page.Auth("anton.golovin@gmail.com", "AntonGolovin90"));
        }

        [TestMethod]
        public void AuthTestWrongPassword()
        {
            var page = new AuthPage();

            Assert.IsFalse(page.Auth("darya.mozgunova@gmail.com", "wrongpass"));
            Assert.IsFalse(page.Auth("andrey.mikhaylenko@gmail.com", "incorrect"));
            Assert.IsFalse(page.Auth("maria.urlova@gmail.com", "12345678"));

            Assert.IsFalse(page.Auth("vasilisa.kaganovskaya@gmail.com", ""));
            Assert.IsFalse(page.Auth("sofya.smirnova@gmail.com", " "));
        }

        [TestMethod]
        public void AuthTestNonExistentUser()
        {
            var page = new AuthPage();

            Assert.IsFalse(page.Auth("nonexistent@test.com", "password"));
            Assert.IsFalse(page.Auth("fake.user@gmail.com", "123456789"));
            Assert.IsFalse(page.Auth("unknown@mail.ru", "moiparol145"));
        }

        [TestMethod]
        public void AuthTestEmptyFields()
        {
            var page = new AuthPage();

            Assert.IsFalse(page.Auth("", ""));
            Assert.IsFalse(page.Auth(" ", " "));
            Assert.IsFalse(page.Auth("darya.mozgunova@gmail.com", ""));
            Assert.IsFalse(page.Auth("", "Choreo12345"));
        }
    }
}
