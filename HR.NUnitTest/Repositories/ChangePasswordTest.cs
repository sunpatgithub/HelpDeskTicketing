using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using NUnit.Framework;
using System;
using HR.WebApi.ModelView;

namespace HR.NUnitTest.Repositories
{
    [TestFixture]
    class ChangePasswordTest
    {
        public IUserService<UserView> userRepository { get; set; }
        public IUserPasswordReset userPasswordResetRepo { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestChangePassword()
        {
            //1. Arrange
            var vPass = new UserController(userRepository, userPasswordResetRepo);
            int user_Id = 0;
            string oldPassword = string.Empty;
            string newPassword = string.Empty;

            //2. Act 
            var vResult = vPass.ChangePassword(user_Id, oldPassword, newPassword);

            //3. Assert
            Assert.IsNotNull(vResult);
        }
    }
}
