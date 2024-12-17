using ChirpCore.Domain;
using ChirpCore.DTOs;
using ChirpInfrastructure;
using ChirpRepositories;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace ChirpServices;


public interface ICheepService
{

	public List<CheepDTO> GetCheeps(int pageNumber);
	public Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string author, int pageNumber);
	public Task<List<CheepDTO>> GetCheepsFromOtherAuthorAsync(string author, int pageNumber);
	//public Task<int> CreateCheepAsync(int userId, string userName, string text);
	public Task<Boolean> ForgetCheepsAsync(string userName);
}

public class CheepService : ICheepService
{
	private readonly ICheepRepository _cheepRepository;
	//private readonly IAuthorRepository _authorRepository;
	private readonly ChirpDBContext _context;

	public CheepService(ICheepRepository cheepRepository, ChirpDBContext context)
	{
		_cheepRepository = cheepRepository;
		_context = context;
	}
	private static readonly List<CheepDTO> _cheeps = [];

	public List<CheepDTO> GetCheeps(int pageNumber)
	{
		_cheeps.Clear();
		var list = _cheepRepository.ReadCheeps(pageNumber);

		//read each CheepObject from CheepRepository
		foreach (CheepDTO cheep in list)
		{
			_cheeps.Add(cheep);
		}

		return _cheeps;
	}

	public async Task<List<CheepDTO>> GetCheepsFromOtherAuthorAsync(string author, int pagenumber)
	{
		_cheeps.Clear();
		var list = await _cheepRepository.ReadCheepsFromFollowListAsync(author, pagenumber);

		//read each CheepObject from CheepRepository
		foreach (CheepDTO cheep in list)
		{
			_cheeps.Add(cheep);
		}

		return _cheeps;

	}
	//Sorts cheep after the string author. We use this for author timelines
	public async Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string author, int pagenumber)

	{
		_cheeps.Clear();
		var list = await _cheepRepository.ReadCheepsFromAuthorAsync(author, pagenumber);

		//read each CheepObject from CheepRepository
		foreach (CheepDTO cheep in list)
		{
			_cheeps.Add(cheep);
		}

		return _cheeps;
	}

	public async Task<Boolean> ForgetCheepsAsync(string userName) {
		try
		{
			await _cheepRepository.ForgetCheepsFromAuthorAsync(userName);
			return true;
		}
		catch (Exception)
		{
			return false;
		 }
	}


	// Method currently not in use, but should be implemented with calls to CheepRepository
	/*
	public async Task<int> CreateCheepAsync(int userId, string userName, string text)
	{

			// should call methods in CheepRepository instead of doing it itseld
			Console.WriteLine($"Creating cheep for user {userId} with text: {text}");

		 // Insert a method to find an author here.

			// Create a new Cheep object (should be given to CheepRepository)

			var newCheep = new Cheep
			{
					CheepId = await _cheepRepository.GenerateNextCheepIdAsync(),
					Author = author,
					Text = text,
					TimeStamp = DateTime.Now
			};

			if (author != null)
			{
					author.Cheeps.Add(newCheep);
			}

			// Add the Cheep to the database context
			await _context.Cheeps.AddAsync(newCheep);
			await _context.SaveChangesAsync();
			return newCheep.CheepId;
	}*/
}
