using System;
using System.Collections.Generic;
using System.Text;

using Xunit.Abstractions;

namespace GroceryManagementxUnitTestProject
{
    public partial class CategoriesApiTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public CategoriesApiTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
    }
}
