using Xunit;
using ChirpInfrastructure;

namespace Chirp.ChirpInfraStructure.Tests
{

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