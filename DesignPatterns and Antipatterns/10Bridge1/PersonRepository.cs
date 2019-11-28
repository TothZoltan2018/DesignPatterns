using System;
using System.Collections.Generic;

namespace _10Bridge1
{
    public class PersonRepository
    {
        List<Person> data = new List<Person>();

        public PersonRepository()
        {
            data.Add(
                new Person
                {
                    Name = "Szuletesnapos kollega",
                    EmailAddress = new EmailAddress { Address = "kollega@hivatali.hu", Display = "Ceges email" },
                }
            );
            data.Add(
                new Person
                {
                    Name = "Masodik kollega",
                    EmailAddress = new EmailAddress { Address = "kollega2@hivatali.hu", Display = "Ceges email" },
                }
            );
            data.Add(
                new Person
                {
                    Name = "Harmadik kollega",
                    EmailAddress = new EmailAddress { Address = "kollega3@hivatali.hu", Display = "Ceges email" },
                }
            );
        }

        public Person GetBirthdayPersons()
        {
            return data[0]; //mindegy mit, csak a pelda kedveert valamit adjunk vissza
        }

        public Person Get(int v)
        {
            throw new NotImplementedException();
        }
    }
}