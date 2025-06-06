﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;

namespace MVC.Controllers
{
    //[Route("ponozky")]
    [Route("socks")]
    public class SocksController : Controller
    {
        //[Authorize]
        public IActionResult Index()
        {

            var data = SocksDataset.GetSocks();

            ViewData["datum"] = DateTime.Now;
          
            return View(data);
        }

        [Route("[action]/{id?}")]
        [Route("[action]")]
        public IActionResult GetById(int? id)
        {
            var data = SocksDataset.GetSocks()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return View(data);
        }

        [Route("[action]/min/{priceMin:int}/max/{priceMax:int}")]
        [Route("[action]")]
        public IActionResult searchByPrice(int priceMin, int priceMax)
        {
            var data = SocksDataset.GetSocks()
                .Where(x => x.Price >= priceMin && x.Price <= priceMax);

            return View("Index", data);
        }
    }
}
