using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using NUnit.Framework;
using System;
using HR.WebApi.ModelView;

namespace HR.NUnitTest.Repositories
{
    [TestFixture]
    class ResetPasswordTest
    {
        public IUserService<UserView> userRepository { get; set; }
        public IUserPasswordReset userPasswordResetRepo { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestResetPassword()
        {
            //1. Arrange
            var vPass = new UserController(userRepository, userPasswordResetRepo);
           
            VerifyLink verifyLink = new VerifyLink
            {
                login_Id = string.Empty,
                token_No = string.Empty,
                password = string.Empty
            };
            
            //2. Act 
            var vResult = vPass.ResetPassword(verifyLink);

            //3. Assert
            Assert.IsNotNull(vResult);
        }
    }
}
