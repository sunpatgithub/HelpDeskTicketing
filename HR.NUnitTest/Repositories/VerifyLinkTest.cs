using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using NUnit.Framework;
using System;
using HR.WebApi.ModelView;

namespace HR.NUnitTest.Repositories
{
    [TestFixture]
    class VerifyLinkTest
    {
        public IUserService<UserView> userRepository { get; set; }
        public IUserPasswordReset userPasswordResetRepo { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestVerifyLink()
        {
            //1. Arrange
            var vPass = new UserController(userRepository, userPasswordResetRepo);         
            VerifyLink verifyLink = new VerifyLink { 
            login_Id = string.Empty,
            token_No = string.Empty
            };
            //2. Act 
            var vResult = vPass.VerifyLink(verifyLink);

            //3. Assert
            Assert.IsNotNull(vResult);// need boolean result
        }

       
        public void Test_IsTrue_VerifyLink(string login_Id, string token_No)
        {
            //1. Arrange
            var vPass = new UserController(userRepository, userPasswordResetRepo);
            VerifyLink verifyLink = new VerifyLink
            {
                login_Id = login_Id,
                token_No = token_No
            };

            //2. Act 
            var vResult = vPass.VerifyLink(verifyLink);

            //3. Assert
            Assert.IsTrue(Convert.ToBoolean(vResult),"Valid Link");// need boolean result
        }

       
        public void Test_IsFalse_VerifyLink(string login_Id, string token_No)
        {
            //1. Arrange
            var vPass = new UserController(userRepository, userPasswordResetRepo);
            VerifyLink verifyLink = new VerifyLink
            {
                login_Id = login_Id,
                token_No = token_No
            };

            //2. Act 
            var vResult = vPass.VerifyLink(verifyLink);

            //3. Assert
            Assert.IsFalse(Convert.ToBoolean(vResult), "InValid Link");// need boolean result
        }
    }
}
