using InsuranceCorp.Data;
using InsuranceCorp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCorp.WEBAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private readonly InsCorpDbContext db;

		public PersonController(InsCorpDbContext db) 
		{
			this.db = db;
		}

		//[Route("list")]
		[HttpGet("list")]
		public ActionResult<List<Person>> List(int start, int take) 
		{
			return db.Persons.Skip(start).Take(take).ToList();
		}
	}
}
