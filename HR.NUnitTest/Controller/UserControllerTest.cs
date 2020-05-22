using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using HR.WebApi.ModelView;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR.NUnitTest.Controller
{
    class UserControllerTest
    {
        private IUserService<UserView> userRepository { get; set; }
        private IUserPasswordReset paginatedQueryRepo { get; set; }

        #region Correct_data
        UserView userView = new UserView
        {
            Emp_Id = 1,
            Company_Id=1,
            Login_Id = "Shahshle",
            User_Type = "Developer",
            Email = "shlesha.shah@bnscoloroma.co.uk",
            Password = "12345",
            PasswordExpiryDate = DateTime.Now.AddDays(30),
            Attempted = 1,
            isLocked = 0,
            LockExpiryTime = null,
            isActive = 1,
            AddedBy = 28,
            AddedOn = DateTime.Now,
            UpdatedBy = 28,
            UpdatedOn = DateTime.Now
        };
        #endregion

        #region data_with_space
        UserView userView_space = new UserView
        {
            Emp_Id = 1,
            Company_Id = 1,
            Login_Id = "Shahshle",
            User_Type = "Developer",
            Email = "shlesha.shah@bnscoloroma.co.uk ",
            Password = "12345",
            PasswordExpiryDate = DateTime.Now.AddDays(30),
            Attempted = 1,
            isLocked = 0,
            LockExpiryTime = null,
            isActive = 1,
            AddedBy = 28,
            AddedOn = DateTime.Now,
            UpdatedBy = 28,
            UpdatedOn = DateTime.Now
        };
        #endregion 

        #region data_with_RegularExpression
        UserView userView_RegularExpression = new UserView
        {
            Emp_Id = 1,
            Company_Id = 1,
            Login_Id = "Shahshle",
            User_Type = "Developer4324",
            Email = "shlesha.shah@bnscoloroma.co.uk",
            Password = "12345",
            PasswordExpiryDate = DateTime.Now.AddDays(30),
            Attempted = 1,
            isLocked = 0,
            LockExpiryTime = null,
            isActive = 1,
            AddedBy = 28,
            AddedOn = DateTime.Now,
            UpdatedBy = 28,
            UpdatedOn = DateTime.Now
        };
        #endregion

        #region VerifyLink
        VerifyLink verifyLink = new VerifyLink
        {
            login_Id = "shahshle",
            token_No = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJnaXZlbl9uYW1lIjoiNWFiY2QwN2EtNTE5OC00ODMxLWE3YWQtNjdiNGJkZWY4NjM0IiwibmJmIjoxNTg5MzY2MTIzLCJleHAiOjE1ODkzNjc5MjMsImlhdCI6MTU4OTM2NjEyMywiaXNzIjoiSXNzdWVkQnkiLCJhdWQiOiJBdWRpZW5jZSJ9.sLMgzcsu245gLG1LeViKYdcDgdyY1Xm9HV8Ng3STxDk",
            password = "12345"

        };
        #endregion

        #region pagination
        Pagination pagination_blank = new Pagination { };

        Pagination pagination_search = new Pagination
        {
            CommonSearch = "\"Home\""
        };
        #endregion pagination

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(500)]
        [Test]
        public void UserController_GetAll(int recordLimit)
        {
            // Arrange
            //int recordLimit = 100;
            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For GetAll
            var response = controller.GetAll(recordLimit);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [TestCase(1)]
        [Test]
        public void UserController_Get(int id)
        {
            // Arrange
            //int id = 1;
            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Get
            var response = controller.Get(id);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [Test]
        public void UserController_Get_FindPagination()
        {
            // Arrange

            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For FindPagination
            var response = controller.FindPagination(pagination_blank);
            //var response1 = controller.FindPagination(pagination_search);

            // Assert the result
            Assert.IsNotNull(response);
            //Assert.IsNotNull(response1);
            Assert.Pass();
        }

        //[Test]
        //public void UserController_GetBy(PaginationBy searchBy)
        //{
        //    // Arrange

        //    // Set up Prerequisites
        //    var controller = new UserController(UserViewRepository, paginatedQueryRepo);

        //    // Act on Test - For Get
        //    var response = controller.GetBy(searchBy);

        //    // Assert the result
        //    Assert.IsNotNull(response);
        //}

        [Test]
        public void UserController_Add()
        {
            // Arrange

            #region Check Validation

            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(userView, new ValidationContext(userView), validationResults, true);

            // Assert
            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
            #endregion

            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Edit
            var response = controller.Add(userView);

            // Assert the result
            Assert.IsNotNull(response);

            Assert.Pass();
        }

        [Test]
        public void UserController_Edit()
        {
            // Arrange

            #region Check Validation

            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(userView, new ValidationContext(userView), validationResults, true);

            // Assert
            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
            #endregion

            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Edit
            var response = controller.Edit(userView);

            // Assert the result
            Assert.IsNotNull(response);

            Assert.Pass();
        }


        [Test]
        public void UserController_UpdateStatus()
        {
            // Arrange
            int id = 0;
            short isActive = 1;
            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For UpdateStatus
            var response = controller.StatusChange(id, isActive);

            // Assert the result
            Assert.IsNotNull(response);
        }
        [Test]
        public void UserController_Delete()
        {
            // Arrange
            int id = 0;
            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Delete
            var response = controller.Delete(id);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [TestCase(1,"abc","12345")]
        [Test]
        public void UserController_ChangePassword(int user_Id, string oldPassword, string newPassword)
        {
            // Arrange
            //int id = 1;
            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Get
            var response = controller.ChangePassword(user_Id, oldPassword,newPassword);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [TestCase("shahshle", "12345")]
        [Test]
        public void UserController_AdminChangePassword(string login_Id, string password)
        {
            // Arrange
            //int id = 1;
            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Get
            var response = controller.AdminChangePassword(login_Id, password);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [TestCase("shahshle")]
        [Test]
        public void UserController_ForgotPassword(string login_Id)
        {
            // Arrange
            //int id = 1;
            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Get
            var response = controller.ForgotPassword(login_Id);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [Test]
        public void UserController_VerifyLink()
        {
            // Arrange

            #region Check Validation

            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(verifyLink, new ValidationContext(verifyLink), validationResults, true);

            // Assert
            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
            #endregion

            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Edit
            var response = controller.VerifyLink(verifyLink);

            // Assert the result
            Assert.IsNotNull(response);

            Assert.Pass();
        }

        [Test]
        public void UserController_ResetPassword()
        {
            // Arrange

            #region Check Validation

            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(verifyLink, new ValidationContext(verifyLink), validationResults, true);

            // Assert
            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
            #endregion

            // Set up Prerequisites
            var controller = new UserController(userRepository, paginatedQueryRepo);

            // Act on Test - For Edit
            var response = controller.VerifyLink(verifyLink);

            // Assert the result
            Assert.IsNotNull(response);

            Assert.Pass();
        }
    }
}
