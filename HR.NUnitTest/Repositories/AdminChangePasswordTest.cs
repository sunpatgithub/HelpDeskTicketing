using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using NUnit.Framework;
using System;
using HR.WebApi.ModelView;

namespace HR.NUnitTest.Repositories
{
    [TestFixture]
    class AdminChangePasswordTest
    {
        public IUserService<UserView> userRepository { get; set; }
        public IUserPasswordReset userPasswordResetRepo { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAdminChangePassword()
        {
            //1. Arrange
            var vPass = new UserController(userRepository, userPasswordResetRepo);
            string login_Id = string.Empty;
            string password = string.Empty;

            //2. Act 
            var vResult = vPass.AdminChangePassword(login_Id, password);

            //3. Assert
            Assert.IsNotNull(vResult);
        }
        
    }
}
