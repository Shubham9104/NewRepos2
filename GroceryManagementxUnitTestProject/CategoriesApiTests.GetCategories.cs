using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;
using Moq;

using Xunit;
using Xunit.Abstractions;
using GroceryManagement.web.Controllers;
using GroceryManagement.web.Models;
using System.Net.Sockets;

namespace GroceryManagementxUnitTestProject
{
    public partial class CategoriesApiTests
    {

        [Fact]
        public void GetCategories_OkResult()
        {
            //saves category to database
            var dbName = nameof(CategoriesApiTests.GetCategories_OkResult);
            //Create the mocked logger that is injected into Categories Controller
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            //invoke controller call
            var controller = new CategoriesController(dbContext, logger);

            IActionResult actionresult = controller.GetCategories().Result;

            Assert.IsType<OkObjectResult>(actionresult);

            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionresult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetCategories_CheckCorrectResult()
        {
            var dbName = nameof(CategoriesApiTests.GetCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new CategoriesController(dbContext, logger);

            IActionResult actionresult = controller.GetCategories().Result;

            Assert.IsType<OkObjectResult>(actionresult);

            var okResult = actionresult.Should().BeOfType<OkObjectResult>().Subject;

            Assert.IsAssignableFrom<List<Category>>(okResult.Value); //error can be found

            var categories = okResult.Value.Should().BeAssignableTo<List<Category>>().Subject;

            Assert.NotNull(categories);

            Assert.Equal(expected: DbContextMocker.TestData_Categories.Length,
                        actual: categories.Count);


            int ndx = 0;
            foreach (Category Category in DbContextMocker.TestData_Categories)
            {
                Assert.Equal<int>(expected: Category.IcId,
                    actual: categories[ndx].IcId);

                Assert.Equal(expected: Category.Categories,
                    actual: categories[ndx].Categories);

                _outputHelper.WriteLine($"Row # {ndx} Okay !!! Issue Id - {Category.IcId} Issue - {Category.Categories}");
                ndx++;
            }

        }

    }
}