using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ChirpCore.Domain;
using Xunit.Sdk;

namespace Chirp.ChirpCore.Tests;

public class UnitTest
{
    [Fact]
    public void CanCreateAuthorWithRequiredProperties()
    {
        ICollection<Cheep> Cheeps = [];
        // Arrange & Act
        var author = new Author
        {
            Name = "Jane Doe",
            Email = "jane.doe@example.com",
            AuthorId = 123,
            Cheeps = Cheeps,
        };

        //Assert
        Assert.Same("Jane Doe", author.Name);
        Assert.Same("jane.doe@example.com", author.Email);
        Assert.Equal(123, author.AuthorId);
        Assert.True(author.Cheeps.Count == 0);
    }

    
    [Fact]
    public void CreatingCheep()
    {
        //Arrange 
        //Pretty stupid to have author creation duplication from CanCreateAuthorWithRequiredProperties
        //test method, but have not figured out shared data in tests yet.
        ICollection<Cheep> Cheeps = [];
        var author = new Author
        {
            Name = "Jane Doe",
            Email = "jane.doe@example.com",
            AuthorId = 123,
            Cheeps = Cheeps,
        };

        Cheep cheep1 = new()
		{
            CheepId = 1,
            AuthorId = 123,
            Author = author,
            Text = "Hvordan lyder Janteloven?",
		    TimeStamp = new DateTime(2024, 11, 13),
        };

        Cheep cheep2 = new()
        {
            CheepId = 2,
            AuthorId = 123,
            Author = author,
            Text = "1. ud af 10. - Du skal ikke tro, du er noget? Bull*",
		    TimeStamp = new DateTime(2024, 11, 14),
        };

        Console.WriteLine(cheep2.TimeStamp);

        //Assert 

        Assert.Same(cheep1.Text, "Hvordan lyder Janteloven?");

        Assert.NotSame(cheep2.Text, cheep1.Text);


    }

    [Fact]
    public void AuthorAndCheepCanBeRelated()
    {
        //Arrange
        ICollection<Cheep> Cheeps = [];
        
        var author = new Author
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            AuthorId = 456,
            Cheeps = Cheeps,
        };

        Cheep cheep3 = new()
		{
            CheepId = 3,
            AuthorId = 456,
            Author = author,
            Text = "2. ud af 10. - Du skal ikke du er lige så meget som os? WTF?",
		    TimeStamp = new DateTime(2024, 11, 12),
        };

        //Act
        author.Cheeps.Add(cheep3);
        
        //Assert 

        Assert.Same(cheep3.Author, author);

        Assert.NotEqual(123, cheep3.AuthorId);

        Assert.NotEqual("Helge", cheep3.Author.Name);

        Assert.Equal(new DateTime(2024, 11, 12), cheep3.TimeStamp);

        Assert.True(author.Cheeps.Contains(cheep3));
    }    

    /* Below two tests can only be implemented when we have a method for creating cheeps.
    [Fact]
    public void CheepCannotExceed160Char(){
        //Arrange
        var failure = false;

        ICollection<Cheep> Cheeps = [];
        
        var author = new Author
        {
            Name = "Jerry Doe",
            Email = "jerry.doe@example.com",
            AuthorId = 789,
            Cheeps = Cheeps,
        };

        try {
            //Use some create/post cheep method here with cheep exceeding 160 chars
        } catch (Exception) {
            failure = true;
        }

        //Assert
        Assert.True(failure);
    }

    [Fact]
    public void CheepCannotBeLessThan1Char(){
        //Arrange
        var failure = false;

        ICollection<Cheep> Cheeps = [];
        
        var author = new Author
        {
            Name = "Jerry Doe",
            Email = "jerry.doe@example.com",
            AuthorId = 789,
            Cheeps = Cheeps,
        };

        try {
            //Use some create/post cheep method here with cheep that is empty
        } catch (Exception) {
            failure = true;
        }
        
        //Assert
        Assert.True(failure);
    }
    */
}