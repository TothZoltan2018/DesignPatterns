using System;
using FluentAssertions; //Nuget PAckage-kent telepitettuk, ez adja a .ShouldThrow metodust
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _01Adapter.Tests
{
    [TestClass]
    public class AdapterExampleTests
    {
        [TestMethod]
        public void ShouldAdapterExampleThrowExceptionIfAllArgumentNull()
        {
            ///AAA
            //Arrange
            
            AdapterExample sut;//sut: System Under Test

            //e helyett:
            //var sut = new AdapterExample(null, null);
            
            //Act
            //rogzitem a muveletet
            Action todo = () => sut = new AdapterExample(null, null);

            //Assert
            todo.ShouldThrow<ArgumentNullException>();
        }
        [TestMethod]
        public void ShouldAdapterExampleThrowExceptionIfFirstArgumentNull()
        {
            ///AAA
            //Arrange

            AdapterExample sut;//sut: System Under Test

            //e helyett:
            //var sut = new AdapterExample(null, null);

            //Act
            //rogzitem a muveletet
            Action todo = () => sut = new AdapterExample(null, new MessageTestService());

            //Assert
            todo.ShouldThrow<ArgumentNullException>();
        }
        [TestMethod]
        public void ShouldAdapterExampleThrowExceptionIfSecondArgumentNull()
        {
            ///AAA
            //Arrange

            AdapterExample sut;//sut: System Under Test

            //e helyett:
            //var sut = new AdapterExample(null, null);

            //Act
            //rogzitem a muveletet
            Action todo = () => sut = new AdapterExample(new AddressTestRepository(), null);

            //Assert
            todo.ShouldThrow<ArgumentNullException>();
        }

    }

}
