using System;
using System.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _01Adapter.Resource;

namespace _01Adapter.Tests
{
    [TestClass]
    public class MockDbDataAdapterTests
    {
        [TestMethod]
        public void MockDbDataAdapterRepositoryShouldThrowExceptionIfCtorArgumentNull()
        {
            //Arrange
            MockDbDataAdapter sut;//sut: System Under Test

            //Act
            Action todo = () => sut = new MockDbDataAdapter(null);

            //Assert
            todo.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void MockDbDataAdapterRepositoryShouldThrowExceptionIfFillArgumentNull()
        {
            //Arrange
            var sut = new MockDbDataAdapter(MockDataTableFactory.GetCreateDataTable());//sut: System Under Test

            //Act
            Action todo = () => sut.Fill(null);

            //Assert
            todo.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void MockDbDataAdapterRepositoryShouldReturnOneTable()
        {
            //Arrange
            var sut = new MockDbDataAdapter(MockDataTableFactory.GetCreateDataTable());//sut: System Under Test
            var dataSet = new DataSet();
            //Act
            sut.Fill(dataSet);

            //Assert
            dataSet.Tables.Should().HaveCount(1);
        }

        [TestMethod]
        public void MockDbDataAdapterRepositoryShouldReturnData()
        {
            //Arrange
            var sut = new MockDbDataAdapter(MockDataTableFactory.GetCreateDataTable());//sut: System Under Test
            var dataSet = new DataSet();
            //Act
            sut.Fill(dataSet);

            //Assert
            dataSet.Tables.Should().HaveCount(1, "Egy tablaval kellett volna visszaterni");

            var table = dataSet.Tables[0];
            MockDataTableFactory.CheckDataTable(table);

        }

    }
}
