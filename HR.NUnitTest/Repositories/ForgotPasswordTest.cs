using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using HR.WebApi.ModelView;
using NUnit.Framework;
using System;

namespace HR.NUnitTest.Repositories
{
    [TestFixture]
    class ForgotPasswordTest
    {
        public IUserService<UserView> userRepository { get; set; }
        public IUserPasswordReset userPasswordResetRepo { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestForgotPassword()
        {
            //1. Arrange
            var vPass = new UserController(userRepository, userPasswordResetRepo);
            string login_Id = string.Empty;

            //2. Act 
            var vResult = vPass.ForgotPassword(login_Id);

            //3. Assert
            Assert.IsNotNull(vResult);

        }        
    }
}
