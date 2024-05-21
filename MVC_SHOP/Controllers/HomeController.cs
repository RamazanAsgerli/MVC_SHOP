using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace MVC_SHOP.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IPersonService _personService;

        public HomeController(IPersonService personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            List<Person> persons = _personService.GetAll();
            return View(persons);
        }

       
    }
}