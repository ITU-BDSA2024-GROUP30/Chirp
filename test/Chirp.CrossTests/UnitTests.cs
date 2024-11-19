namespace Chirp.CrossTests;

public class UnitTests
{

	[Fact]
	public void TestThatCanBeDeleted()
	{
		//Arrange
		string h2 = "Hello World!";

		//ACt

		//Assert
		Assert.Same("Hello World!", h2);

	}
}
