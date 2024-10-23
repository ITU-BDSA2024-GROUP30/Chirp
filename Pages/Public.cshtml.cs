using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System;

namespace Chirp.Pages;
//[ApiController]
//[Route("api/[controller]")]
public class PublicModel : PageModel
{

	private readonly ICheepService _service;
	public List<CheepObject> Cheeps { get; set; }
	public int current;


	public PublicModel(ICheepService service)
	{
		_service = service;
	}
	/*[HttpGet]
	public PagedResult<T> GetPagedData<T>(IQueryable<T> query, int page, int pageSize) where T :class
	{

		//var pagedQuery = "SELECT *,FROM data/chirp.db, ORDERBY message";

		//var pagniatedResult = pagedQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

		//var dataResult = "/data/chirp.db".Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

		var result = new PagedResult<T>
		{
			CurrentPage = page,
			pageSize = pageSize,
			RowCount = query.Count()
		};

		var pageCount =(double)result.RowCount / pageSize;
		result.PageCount =(int)Math.Ceiling(pageCount);

		var skip =(page - 1) * pageSize;
		result.Results = query.Skip(skip).Take(pageSize).ToList();

		return result;
	}*/


	public async Task<ActionResult> OnGetAsync([FromQuery] int page)
	{
		current = page;
		Cheeps = await  _service.GetCheeps(current);

		if (current < 1)
		{
			current = 1;
		}
		
		return Page();
	}

}
