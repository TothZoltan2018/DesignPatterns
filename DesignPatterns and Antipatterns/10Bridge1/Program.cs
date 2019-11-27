using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10Bridge1
{
    /// <summary>
    /// Adapter minta: implementacio UTAN, ket meglevo osztaly osszekotesehez hasznaljuk
    /// (Ertelmes modon, az ujrafelhasznalhatosag megtartasaval osszekotunk ket egymassal
    /// jelenleg nem egyuttmukodo osztalyt)
    /// 
    /// Hid minta: implementacio ELOTT eleve olyan szerkezetet hozunk letre, ahol gyege csatolas miatt
    /// az egyes elemek lecserelhetoek
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //A hid  minta bevezetesehez es tesztjehez
            //TestBridge1();

            var officeAddress = new EmailAddress { Address = "iroda@hivatali.hu", Display = "Az iroda email cime" };

            //Elore tudom, hogy hidat akarok hasznalni,
            //levalasztom a konkret megvalositast a hasznalattol
            //ez az adatok tarolasanal a repository minta
            var repo = new PersonRepository();

            //Ezek csak peldak; ilyeneket lehetne csinalni egy repoban
            //var person = repo.Get(1); 
            //var person = repo.Create(person);
            //var person = repo.Delete(person);
            //var person = repo.Update(person);

            var person = repo.GetBirthdayPersons();

            var sendWith = new SendWith(); //strategia
            var service = new EmailService(sendWith);

            var message = new EmailMessage
            {
                From = officeAddress,
                To = person.EmailAddress,
                Suject = "tesztuzenet",
                Message = "Ez egy tesztuzenet, amit egy kuld a kettonek."
            };

            service.Send(message);

            Console.ReadLine();
        }

        private static void TestBridge1()
        {
            var message = new EmailMessage
            {
                From = new EmailAddress { Address = "egy@teszt.hu", Display = "az elso cim" },
                To = new EmailAddress { Address = "ketto@teszt.hu", Display = "a masodik cim " },
                Suject = "Udvozlet",
                Message = "Boldog szuletesnapot!"
            };

            ///////////////////////////////////////////
            //Concrete Implementor
            var strategy = new SendWith();

            //abstraction
            var service = new EmailService(strategy);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////

            //Concrete Implementor
            var strategyMsx = new SendWithExchange();
            strategyMsx.Host = "1.1.1.1";
            strategyMsx.UserName = "MSXUser";
            strategyMsx.Password = "MSXPassword";

            //Nem kell uj service tipust peldanyositani!
            service = new EmailService(strategyMsx);
            service.Send(message);
            Console.WriteLine();

            ///////////////////////////////////////////
            //Concrete Implementor
            var strategySG = new SendWithSendGrid();
            strategySG.HostUrl = "https://sendgrid.service.com";
            strategySG.ApiKey = "SG-APIKEY";

            service = new EmailService(strategySG);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////
            //Concrete Implementor
            var strategyM = new SendWithMandrill();
            strategyM.HostUrl = "https://api.mandrill.com";
            strategyM.ClientSecret = "MANDRILL-SECRET";
            strategyM.ClientKey = "MANDRILL-KEY";

            service = new EmailService(strategyM);
            service.Send(message);
            Console.WriteLine();
        }
    }
}
