using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using NUnit.Framework;
using System.Linq;

namespace HR.NUnitTest.Controller
{
    [TestFixture]
    class EmailConfigControllerTest
    {

        private ICommonRepository<Email_ConfigView> Email_ConfigRepository { get; set; }
        private ICommonQuery<Email_ConfigView> commonQueryRepo { get; set; }

        private Email_ConfigController controller;

        #region Data
        #region Correct_data
        Email_ConfigView Correct_data = new Email_ConfigView
        {
           Company_Id = 1,
           Email_Host ="noreply@somthing.co.uk",
           Email_Password = "somawoudb09",
           Email_Port = 853,
           Email_UserName = "Company",
           EnableSSL = 1,
           TLSEnable = 1,
           isActive = 1,
           AddedBy = 18

        };
        #endregion Correct_data

        #region Incorrect_Data
        Email_ConfigView Incorrect_Data = new Email_ConfigView
        {
            Company_Id = 1,
            Email_Host = "noreply@somthing.co.uk",
            //Email_Password = "somawoudb09",
            Email_Port = 853,
            //Email_UserName = "Company",
            EnableSSL = 1,
            TLSEnable = 1,
            isActive = 1,
            AddedBy = 18
        };
        #endregion Incorrect_Data

        #region pagination
        Pagination pagination_blank = new Pagination { };

        Pagination pagination_search = new Pagination
        {
            CommonSearch = "\"email\""
        };
        #endregion pagination
        #endregion Data

        [SetUp]
        public void Setup()
        {
            controller = new Email_ConfigController(Email_ConfigRepository, commonQueryRepo);
        }

        [TestCase(500)]
        [Test]
        public void Email_ConfigController_GetAll(int recordLimit)
        {
            // Arrange

            // Set up Prerequisites

            // Act on Test - For GetAll
            var response = controller.GetAll(recordLimit);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [TestCase(1)]
        [Test]
        public void Email_ConfigController_Get(int id)
        {
            // Arrange

            // Set up Prerequisites

            // Act on Test - For Get
            var response = controller.Get(id);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [Test]
        public void Email_ConfigController_FindPagination()
        {
            // Arrange

            // Set up Prerequisites

            // Act on Test - For FindPagination
            var response = controller.FindPagination(pagination_blank);
            //var response1 = controller.FindPagination(pagination_search);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [Test]
        public void Email_ConfigController_Add()
        {
            // Arrange

            // Set up Prerequisites
            CheckPropertyValidation cpv = new CheckPropertyValidation();

            // Act on Test - For Add
            var response = controller.Add(Correct_data);
            var errorcount = cpv.CheckValidation(Correct_data);
            var errorcount1 = cpv.CheckValidation(Incorrect_Data);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.AreEqual(0, errorcount.Count, "Test Performed Successfully.");
            Assert.AreNotEqual(0, errorcount1.Count, "Total validation error : " + errorcount1.Count.ToString());

            Assert.Pass();
        }

        [Test]
        public void Email_ConfigController_Edit()
        {
            // Arrange

            // Set up Prerequisites
            CheckPropertyValidation cpv = new CheckPropertyValidation();

            // Act on Test - For Edit
            var response = controller.Edit(Correct_data);
            var errorcount = cpv.CheckValidation(Correct_data);
            var errorcount1 = cpv.CheckValidation(Incorrect_Data);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.AreEqual(0, errorcount.Count, "Test Performed Successfully.");
            Assert.AreNotEqual(0, errorcount1.Count, "Total validation error : " + errorcount1.Count.ToString());

            Assert.Pass();
        }

        [TestCase(1, 0)]
        [Test]
        public void Email_ConfigController_UpdateStatus(int id, short isActive)
        {
            // Arrange

            // Set up Prerequisites

            // Act on Test - For UpdateStatus
            var response = controller.UpdateStatus(id, isActive);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [TestCase(1)]
        [Test]
        public void Email_ConfigController_delete(int id)
        {
            // Arrange

            // Set up Prerequisites

            // Act on Test - For Delete
            var response = controller.Delete(id);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }
    }
}
