using HR.WebApi.Controllers;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using HR.WebApi.ModelView;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR.NUnitTest.Controller
{
    [TestFixture]
    class EmployeeControllerTest
    {     
      
        public ICommonRepository<Employee> employeeRepository { get; set; }
        public IPaginated<Employee> paginatedQueryRepo { get; set; }

        #region Correct_data
        Employee employee = new Employee
        {
            Company_Id = 1,
            Site_Id = 1,
            JD_Id = 1,
            Dept_Id = 1,
            Desig_Id = 1,
            Zone_Id = 1,
            Shift_Id = 1,
            Emp_Code = "E-1",
            JoiningDate = DateTime.Now,
            Reporting_Id = 1,
            isSponsored = 1,
            Tupe = 0,
            NiNo = string.Empty,
            PreviousEmp_Id = 0,
            isActive = 1,
            AddedBy = 1,
            AddedOn = DateTime.Now
        };
        #endregion Correct_data

        #region data_with_space
        Employee employee_space = new Employee
        {
            Company_Id = 1,
            Site_Id = 1,
            JD_Id = 1,
            Dept_Id = 1,
            Desig_Id = 1,
            Zone_Id = 1,
            Shift_Id = 1,
            Emp_Code = " E-1 ",
            JoiningDate = DateTime.Now,
            Reporting_Id = 1,
            isSponsored = 1,
            Tupe = 0,
            NiNo = string.Empty,
            PreviousEmp_Id = 0,
            isActive = 1,
            AddedBy = 1,
            AddedOn = DateTime.Now
        };
        #endregion data_with_space

        #region data_with_RegularExpression
        Employee employee_RegularExpression = new Employee
        {
            Company_Id = 1,
            Site_Id = 1,
            JD_Id = 1,
            Dept_Id = 1,
            Desig_Id = 1,
            Zone_Id = 1,
            Shift_Id = 1,
            Emp_Code = " E.1",
            JoiningDate = DateTime.Now,
            Reporting_Id = 1,
            isSponsored = 1,
            Tupe = 0,
            NiNo = string.Empty,
            PreviousEmp_Id = 0,
            isActive = 1,
            AddedBy = 1,
            AddedOn = DateTime.Now
        };
        #endregion data_with_space

        #region pagination
        Pagination pagination_blank = new Pagination { };

        Pagination pagination_search = new Pagination
        {
            CommonSearch = "\"Shlesha\""
        };
        #endregion pagination

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(500)]
        [Test]
        public void EmployeeController_GetAll(int recordLimit)
        {
            // Arrange
            
            // Set up Prerequisites
            var controller = new EmployeeController(employeeRepository, paginatedQueryRepo);            

            // Act on Test - For GetAll
            var response = controller.GetAll(recordLimit);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

        [TestCase(1)]
        [Test]
        public void EmployeeController_Get(int id)
        {
            // Arrange
            
            // Set up Prerequisites
            var controller = new EmployeeController(employeeRepository, paginatedQueryRepo);

            // Act on Test - For Get
            var response = controller.Get(id);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }
        
        [Test]
        public void EmployeeController_Get_FindPagination()
        {
            // Arrange

            // Set up Prerequisites
            var controller = new EmployeeController(employeeRepository, paginatedQueryRepo); 

            // Act on Test - For FindPagination
            var response = controller.FindPagination(pagination_blank);
            //var response1 = controller.FindPagination(pagination_search);

            // Assert the result
            Assert.IsNotNull(response);
            //Assert.IsNotNull(response1);
            Assert.Pass();
        }

        //[Test]
        //public void EmployeeController_GetBy(PaginationBy searchBy)
        //{
        //    // Arrange

        //    // Set up Prerequisites
        //    var controller = new EmployeeController(employeeRepository, paginatedQueryRepo);

        //    // Act on Test - For Get
        //    var response = controller.GetBy(searchBy);

        //    // Assert the result
        //    Assert.IsNotNull(response);
        //}

        [Test]
        public void EmployeeController_Edit()
        {
            // Arrange

            #region Check Validation
           
            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(employee, new ValidationContext(employee), validationResults, true);

            // Assert
            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
            #endregion

            // Set up Prerequisites
            var controller = new EmployeeController(employeeRepository, paginatedQueryRepo);

            // Act on Test - For Edit
            var response = controller.Edit(employee);

            // Assert the result
            Assert.IsNotNull(response);

            Assert.Pass();
        }
      

        [Test]
        public void EmployeeController_UpdateStatus()
        {
            // Arrange
            int id=0;
            short isActive=1;
            // Set up Prerequisites
            var controller = new EmployeeController(employeeRepository, paginatedQueryRepo);

            // Act on Test - For UpdateStatus
            var response = controller.UpdateStatus(id,isActive);

            // Assert the result
            Assert.IsNotNull(response);
        }
        [Test]
        public void EmployeeController_Delete()
        {
            // Arrange
            int id = 0;
            // Set up Prerequisites
            var controller = new EmployeeController(employeeRepository, paginatedQueryRepo);

            // Act on Test - For Delete
            var response = controller.Delete(id);

            // Assert the result
            Assert.IsNotNull(response);
            Assert.Pass();
        }

    }
}

