using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10Bridge1
{
    public class PersonRepositorySimpleData : IPersonRepository
    {
        public Person GetBirthdayPersons()
        {
            return new Person
            {
                Name = "Kollega a 'Simple' repo-bol",
                EmailAddress = new EmailAddress { Address = "kollegaSimpleRepo@hivatali.hu", Display = "Ceges email" }
            };
        }

        public List<Person> GetPersonForMessages()
        {
            return new List<Person>(new Person[] {
                new Person
                {
                    Name = "Kollega a 'Simple' repo-bol",
                    EmailAddress = new EmailAddress { Address = "kollegaSimpleRepo@hivatali.hu", Display = "Ceges email" }
                }
            });
        }
    }
}
