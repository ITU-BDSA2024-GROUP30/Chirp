using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;
using ChirpCore.Domain;
using ChirpInfrastructure;

namespace Chirp.ChirpInfraStructure.Tests{

public class UnitTest
{
    
    [Fact]
    public void TestThatTestWork()
    {
       //Arrange
        string h = "Hej";

        //ACt

        //Assert
        Assert.Same("Hej", h);

    }
}
}