using Business.CustomExceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_SHOP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            List<Person> people=_personService.GetAll();
            return View(people);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _personService.AddPerson(person);
            }
            catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }

            catch(FileLengthException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _personService.DeletePerson(id);
            }
            catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(PersonNotFoundException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
            
        }

        public IActionResult Update(int id)
        {
            var person = _personService.GetPerson(x => x.Id == id);
            if(person == null)
            {
                ModelState.AddModelError("", "sss");
                return View();
            }
            return View(person);
        }

        [HttpPost]

        public IActionResult Update(Person person)
        {
            try
            {
                _personService.UpdatePerson(person.Id, person);
            }
            catch (EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch (ContentTypeException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }

            catch (FileLengthException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));


        }

    }
}
