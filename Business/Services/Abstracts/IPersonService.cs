using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IPersonService
    {
        void AddPerson(Person person);
        void DeletePerson(int id);
        void UpdatePerson(int id,Person person);
        Person GetPerson(Func<Person,bool>? func=null);
        List<Person> GetAll(Func<Person,bool>? func=null);
    }
}
