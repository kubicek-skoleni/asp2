using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
	[Route("[controller]")]
	public class AnimalsController : Controller
	{
		[Route("[action]/{count:int?}")]
		public string List(int? count)
		{
			return $"Seznam všech zvířat, omezeno na {count}.";
		}
	}
}
