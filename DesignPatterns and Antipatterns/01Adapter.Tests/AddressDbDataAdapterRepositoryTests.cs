using System;
using System.Data;
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
        public void AddressDbDataAdapterRepositoryShouldReturnData()
        {
            //Arrange
            
            var adapter = new MockDbDataAdapter(MockDataTableFactory.GetCreateDataTable());
            var sut = new AddressDbDataAdapterRepository(adapter);

            //Act
            var list = sut.GetAddresses();

            //Assert
            list.Should().HaveCount(1, "Mivel egy elemet kuldtunk a repoba")
                .And
                .Should().Equals(new Address { Email = GlobalStrings.TestEmailAddress });
        }
    }
}
