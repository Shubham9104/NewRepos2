using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using Moq;

using Xunit;
using Xunit.Abstractions;
using GroceryManagement.web.Controllers;
using GroceryManagement.web.Models;
using System.Linq;

namespace GroceryManagementxUnitTestProject
{
    public partial class CategoriesApiTests
    {
        [Fact]
        public void GetCategoryByID_NotFoundResult()
        {
            var dbName = nameof(CategoriesApiTests.GetCategoryByID_NotFoundResult);
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new CategoriesController(dbContext, logger);
            int findCategoryID = 900;

            IActionResult actionresult = controller.GetCategory(findCategoryID).Result;

            Assert.IsType<NotFoundResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound; //404
            var actualStatusCode = (actionresult as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }


        [Fact]
        public void GetCategoryByID_BadFoundResult()
        {
            var dbName = nameof(CategoriesApiTests.GetCategoryByID_BadFoundResult);
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new CategoriesController(dbContext, logger);
            int? findCategoryID = null;

            IActionResult actionresult = controller.GetCategory(findCategoryID).Result;

            Assert.IsType<BadRequestResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest; //404
            var actualStatusCode = (actionresult as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }



        [Fact]
        public void GetCategoryById_OkResult()
        {

            var dbName = nameof(CategoriesApiTests.GetCategoryById_OkResult);
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new CategoriesController(dbContext, logger);
            int findCategoryID = 1;

            IActionResult actionresult = controller.GetCategory(findCategoryID).Result;

            Assert.IsType<OkObjectResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK; //200
            var actualStatusCode = (actionresult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetCategoryById_CorrectResult()
        {

            var dbName = nameof(CategoriesApiTests.GetCategoryById_CorrectResult);
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new CategoriesController(dbContext, logger);
            int findCategoryID = 2;

            Category expectedCategory = DbContextMocker.TestData_Categories
                                            .SingleOrDefault(c => c.IcId == findCategoryID);



            IActionResult actionresult = controller.GetCategory(findCategoryID).Result;

            OkObjectResult result = actionresult.Should().BeOfType<OkObjectResult>().Subject;

            Assert.IsType<Category>(result.Value);

            Category pc = result.Value.Should().BeAssignableTo<Category>().Subject;//actual category
            _outputHelper.WriteLine($"Found: Category Id : {pc.IcId}, Category Name : {pc.Categories}");

            Assert.NotNull(pc);



            Assert.Equal<int>(expected: expectedCategory.IcId, actual: pc.IcId);


            Assert.Equal(expected: expectedCategory.Categories, actual: pc.Categories);


        }



    }
}