using System;
using System.Data;
using System.Data.OleDb;
using _01Adapter.Resource;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _01Adapter.Tests
{
    /// <summary>
    /// Repository ----()-----> Adatok (adatbazis)
    /// A fenti eros csatolas helyett indirekcioval:
    /// Repository ----> IDbDataAdapter  -----Adatok (adatbazis)
    /// 
    /// </summary>


    [TestClass]
    public class AddressDbDataAdapterRepositoryTests
    {
        [TestMethod]
        public void AddressDbDataAdapterRepositoryShouldThrowExceptionIfArgumentNull()
        {
            //Arrange
            AddressDbDataAdapterRepository sut;//sut: System Under Test

            //Act
            Action todo = () => sut = new AddressDbDataAdapterRepository(null);

            //Assert
            todo.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void AddressDbDataAdapterRepositoryShouldReturnMockData()
        {
            //Arrange
            
            var adapter = new MockDbDataAdapter(MockDataTableFactory.GetCreateDataTable());
            var sut = new AddressDbDataAdapterRepository(adapter);

            //Act
            var list = sut.GetAddresses();

            //Assert
            list.Should().HaveCount(1, "Mivel egy elemet kuldtunk a repoba");
                //.And
                //.Should().Equals(new Address { Email = GlobalStrings.TestEmailAddress });
        }

        [TestMethod]
        public void AddressDbDataAdapterRepositoryShouldReturnSQLData()
        {
            //Arrange

            var adapter = new OleDbDataAdapter();
            adapter.SelectCommand = new OleDbCommand($"SELECT * FROM {GlobalStrings.TableName}");
            adapter.SelectCommand.Connection = new OleDbConnection("Provider=sqloledb;Data Source=.\\sqlexpress;Initial Catalog=00Data.AddressContext;Integrated Security = SSPI;");
            
            var sut = new AddressDbDataAdapterRepository(adapter);

            //Act
            var list = sut.GetAddresses();

            //Assert
            list.Should().HaveCount(1, "Mivel egy elemet kuldtunk a repoba");
                //.And
                //.Should().Equals(new Address { Email = GlobalStrings.TestEmailAddress });
        }
    }
}
