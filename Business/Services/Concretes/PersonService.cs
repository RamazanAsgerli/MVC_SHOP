using Business.CustomExceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void AddPerson(Person person)
        {
            if (person == null) throw new EntityNullException("", "entity is null");
            if (person.PhotoFile == null) throw new FileNotFoundException("PhotoFile", "File not found");
            if (!person.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "ss");
            if (person.PhotoFile.Length > 2098153) throw new FileLengthException("PhotoFile", "Length cox ola bilmez");
           
            string path = "C:\\Users\\ll novbe\\Desktop\\MVC_SHOP\\MVC_SHOP\\wwwroot\\Upload\\Person\\" + person.PhotoFile.FileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
            person.PhotoFile.CopyTo(stream);
            }
            person.ImgUrl = person.PhotoFile.FileName;
            _personRepository.Add(person);
            _personRepository.Commit();
            



        }

        public void DeletePerson(int id)
        {
            var person = _personRepository.Get(x => x.Id == id);
            if (person == null) throw new EntityNullException("", "Person not found");
            string path = "C:\\Users\\ll novbe\\Desktop\\MVC_SHOP\\MVC_SHOP\\wwwroot\\Upload\\Person\\" + person.ImgUrl;
            if (!File.Exists(path)) throw new PersonNotFoundException("", "person not found");
            File.Delete(path);
            _personRepository.Delete(person);
            _personRepository.Commit();
        }

        public List<Person> GetAll(Func<Person, bool>? func = null)
        {
            return _personRepository.GetAll(func);
        }

        public Person GetPerson(Func<Person, bool>? func = null)
        {
            return _personRepository.Get(func);
        }

        public void UpdatePerson(int id, Person person)
        {
            var oldPerson=_personRepository.Get(x => x.Id == id);
            if (oldPerson == null) throw new EntityNullException("", "Entity null!!!!!!!!!");
            if (person == null) throw new EntityNullException("", "Entity");
            if (person.PhotoFile != null)
            {
                if (!person.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "ss");
                if (person.PhotoFile.Length > 2098153) throw new FileLengthException("PhotoFile", "Length cox ola bilmez");
                string path = "C:\\Users\\ll novbe\\Desktop\\MVC_SHOP\\MVC_SHOP\\wwwroot\\Upload\\Person\\" + oldPerson.ImgUrl;
                if (!File.Exists(path)) throw new PersonNotFoundException("", "person not found");
                File.Delete(path);

                string path2 = "C:\\Users\\ll novbe\\Desktop\\MVC_SHOP\\MVC_SHOP\\wwwroot\\Upload\\Person\\" + person.PhotoFile.FileName;
                using (FileStream stream = new FileStream(path2, FileMode.Create))
                {
                    person.PhotoFile.CopyTo(stream);
                }
                oldPerson.ImgUrl = person.PhotoFile.FileName;
            }
            oldPerson.FullName=person.FullName;
            oldPerson.Title = person.Title;
            _personRepository.Commit();

        }
    }
}
