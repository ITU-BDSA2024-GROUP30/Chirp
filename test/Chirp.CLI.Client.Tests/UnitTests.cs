namespace Chirp.CLI.Client.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }
}

/*using System.Reflection;
using FluentAssertions;


namespace ArchitectureTests;

//Packages
// %dotnet add package FluentAssertions --version 6.12.1
// %dotnet add package NetArchTest.Rules



[ArchitectureTestsClass]
public class UnitTest1
{
    private const string SimpleDBNameSpace = "SimpleDB";
    private const string ProgramNameSpace = "Program";
    private const string IDatabaseRepositoryNameSpace = "IDatabaseRepository";

    
    public void Test1()
    {
        //Arrange
     
        var assembly1 = Assembly.Load("SimpleDB");

        var assembly2 = Assembly.Load("Program");
        var assembly3 = Assembly.Load("IDatabaseRepository");
        
        
        //Act
        var testResult = new[]{
            assembly2,
            assembly3
        };
       
        //Assert

        //assembly1.Should().NotContain(assembly2);
        testResult.Should().NotContain(assembly1);
        //Assembly1.ContainValue("SimpleDB");
        //testResult.ContainValue(2);
    }

    
    // This test ensures that the Chirp CLI can successfully parse command-line arguments.
    public void Test2()
    {
        //Arrange
        var args = new[] { "-r", "50", "-c", "Hello", "Chirp" };  // Example args for reading and writing cheeps.
        
        // Redirect console output to a StringWriter
        using (var consoleOutput = new StringWriter())
        {
            Console.SetOut(consoleOutput);

            // Act
            Chirp.Cli.Program.Main(args);

            // Assert
            var output = consoleOutput.ToString();
            
            output.Should().NotBeNullOrEmpty();
            output.Should().Contain("Hello Chirp");  // Check if the cheep was processed and displayed.
        }
    }
    /*  Simulates a Chirp CLI execution with command-line arguments for both reading (-r 50) and cheeping (-c Hello Chirp).
        Redirects console output to verify the results.
        Checks if the output contains the message "Hello Chirp", confirming the application processes the input correctly.
    */



// This test checks if the Chirp CLI stores and reads data correctly in the SimpleDB (CSVDatabase).
/*public void Test3()
{
    //Arrange
    string testFilePath = "test_chirp_cli_db.csv";

    // Clean up any existing test file
    if (File.Exists(testFilePath))
    {
        File.Delete(testFilePath);
    }

    var cheep = new Chirp.Cli.Program.Cheep
    {
        Author = "TestUser",
        Message = "This is a test cheep",
        Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
    };

    IDatabaseRepository<Chirp.Cli.Program.Cheep> csvDB = new Chirp.Cli.SimpleDB.CSVDatabase<Chirp.Cli.Program.Cheep>();

    //Act
    csvDB.Store(cheep, testFilePath);
    var storedCheeps = csvDB.Read(1, testFilePath).ToList();

    //Assert
    storedCheeps.Should().ContainSingle();
    storedCheeps.First().Author.Should().Be("TestUser");
    storedCheeps.First().Message.Should().Be("This is a test cheep");
}
}
/*  Tests the storage and reading of cheeps in the CSVDatabase.
Creates a test cheep and stores it in a CSV file.
Reads the cheep back and verifies its contents to ensure the database functionality works as expected. 
*/

/*internal class ArchitectureTestsClassAttribute : Attribute
{
}
*/