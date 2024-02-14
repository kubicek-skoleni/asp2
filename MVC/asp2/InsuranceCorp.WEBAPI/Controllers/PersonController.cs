using InsuranceCorp.Data;
using InsuranceCorp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCorp.WEBAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
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

		[HttpGet("{id:int}")]
		public ActionResult<Person> Get(int id) 
		{
			var person = db.Persons
					.Include(x => x.Address)
					.Include(x => x.Contracts)
					.FirstOrDefault(x =>  x.Id == id);

			if (person == null)
			{
				return NotFound();
			}

			return person;
		}

		[HttpGet("SerchByEmail/{email}")]
		public ActionResult<List<Person>> SearchByEmail(string email)
		{
			return db.Persons.Where(x => !string.IsNullOrEmpty(x.Email)
										&& x.Email.ToLower().Contains(email.ToLower()))
				.ToList();
		}

		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public ActionResult Create(Person person) 
		{
			db.Persons.Add(person);
			db.SaveChanges();
			return Created($"/api/person/{person.Id}", person);
		}
	}
}
