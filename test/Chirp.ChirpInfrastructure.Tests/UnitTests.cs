using Xunit;
using ChirpInfrastructure;

namespace Chirp.ChirpInfraStructure.Tests{

public class UnitTest1
{
    
    [Fact]
    public void Test1()
    {
       //Arrange
        string h = "Hej";

        //ACt

        //Assert
        Assert.Same("Hej", h);

    }
}
}