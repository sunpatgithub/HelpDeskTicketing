using NUnit.Framework;
using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using HR.WebApi.ModelView;


namespace HR.NUnitTest.Controller
{
    [TestFixture]
    class DesignationControllerTest
    {
        public ICommonRepository<DesignationView> designationRepository { get; set; }
        public ICommonQuery<DesignationView> commonQueryRepo { get; set; }

        private DesignationController controller;

        #region Data
        #region Correct_data
        DesignationView Correct_data = new DesignationView
        {
            Company_Id = 1,
            Dept_Id = 1,
            Zone_Id = 1,
            Desig_Code = "111sd",
            Desig_Name = "Software Developer",
            Report_To = "Someone",
            AdditionalReport_To = "Someone",
            isActive = 1,
            AddedBy = 18,
        };
        #endregion Correct_data

        #region Incorrect_Data
        DesignationView Incorrect_Data = new DesignationView
        {
            //Company_Id = 1,
            //Dept_Id = 1,
            //Zone_Id = 1,
            Desig_Code = "111 sd",
            Dept_Name = " @!Software Developer",
            Report_To = "Someone",
            AdditionalReport_To = "Someone",
            //isActive = 1,
            //AddedBy = 18,
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
            controller = new DesignationController(designationRepository, commonQueryRepo);
        }

        [TestCase(500)]
        [Test]
        public void DesignationController_GetAll(int recordLimit)
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
        public void DesignationController_Get(int id)
        {
            // Arrange

            // Set up Prerequisites
            var controller = new DesignationController(designationRepository, commonQueryRepo);

            // Act on Test - For Get
            var response = controller.Get(id);

            // Assert the result
            Assert.IsNotNull(response);
            //Assert.IsNotNull(response.Content);
            //Assert.AreEqual(1, response.Content.Dept_Id);

            //Assert.Pass();
        }

        [Test]
        public void DesignationController_FindPagination()
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
        public void DesignationController_Add()
        {
            // Arrange

            // Set up Prerequisites
            CheckPropertyValidation cpv = new CheckPropertyValidation();

            // Act on Test - For Post
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
        public void DesignationController_Edit()
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
        public void DesignationController_UpdateStatus(int id, short isActive)
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
        public void DesignationController_Delete(int id)
        {
            // Arrange

            // Set up Prerequisites

            // Act on Test - For Post
            var response = controller.Delete(id);

            // Assert the result
            Assert.IsNotNull(response);

            //Assert.Pass();
        }
    }
}
