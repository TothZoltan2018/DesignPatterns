using System;
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
    }
}
