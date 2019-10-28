using System;
using System.Data;
using _01Adapter.Resource;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _01Adapter.Tests
{
    [TestClass]
    public class MockDataTableFactoryTests
    {
        [TestMethod]
        public void MockDataTableFactoryShouldReturnData()
        {
            //Arrange
            //Act
            var sut = MockDataTableFactory.GetCreateDataTable();//sut: System Under Test

            //Assert
            MockDataTableFactory.CheckDataTable(sut);

        }
    }
}
