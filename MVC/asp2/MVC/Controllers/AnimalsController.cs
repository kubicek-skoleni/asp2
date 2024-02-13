using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
	[Route("[controller]")]
	public class AnimalsController : Controller
	{
		[Route("[action]/{count:int?}")]
		public string List(int? count)
		{
			string[] names = { "Pavel", "Jana", "Veronika" };


			return $"Seznam všech zvířat, omezeno na {count}.";
		}
	}
}
