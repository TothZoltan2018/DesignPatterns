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

            var sendWith = SendWith.SendWithFactory(); //statikus fgv-t keszitettunk
            var service = new EmailService(sendWith); //Fogadja a strategiat (a Send metodust) es meghivja

            //----------------------------------######## DEKORATOR #######---------------------------------------------------------------------
            //keszitunk egy olyan szervizt, ami naplot is keszit
            //Ha a szerviz kodjat mar valamiert nem modosithatjuk (pl. regi kod kiegeszitese, vagy nem modosithato a forraskod),
            //akkor ezt ######## DEKORATOR ####### mintaval tudjuk megtenni
            //Keszitunk egy burkoloosztalyt, es dependency injection-el atadjuk neki az eredeti osztalyt. 

            //Akkor mukodik, ha a EmailServiceWithLogger feluletet tudom hasznalni.
            //Vagy, ha a dekoralando fgv (Send) virtualis, es overridolni tudom a dekorator osztalyban.             
            //EmailService serviceWithLogger = new EmailServiceWithLogger(service, sendWith);// --> Igy az ososztaly Send fuggvenyet fogja hivni, ha nincs virt/overr.
            var serviceWithLogger = new EmailServiceWithLogger(service, sendWith);

            //----------------------------------######### PROXY ############---------------------------------------------------------------------
            //A proxy osztaly feluletenek hasznalatat ki lehet kenyszeriteni
            //A Decorator-hoz hasonloan ez is egy wrapper (beburkoljuk es meghivjuk az eredeti osztalyt, es annak mukodeset kiegeszitjuk)
            var serviceWithProxy = new EmailServiceWithProxy(service, sendWith);

            //A proxy osztalyt akkor hasznaljuk, ha pl.:
            // - a fejleszteskor nem all rendelkezesre a vegleges megvalositas
            // - halozaton keresztul kapcsolodunk, es szeretnenk tesztet irni,
            // - jogosultsagot implementalni,
            // - cache-t implementalni

            // ----------------------------------######### FACADE ############---------------------------------------------------------------------
            //Amikor az eredeti osztaly felulete tul bonzolult, akkor helyette egy konnyebben felhasznalhato feluletet adunk. Pl.:
            // - Sok, komolyabb (sok lepesbol allo) workflow-t implementalo WebAPI as sajat klienskonyvtarat. Pl.:
            //        - PayPal fizetes
            //---------------------------------------------------------------------------------------------------------------------------------------
            var message = new EmailMessage
            {
                From = officeAddress,
                To = person.EmailAddress,
                Subject = "tesztuzenet",
                Message = "Ez egy tesztuzenet, amit egy kuld a kettonek."
            };

            //service.Send(message);
            serviceWithLogger.Send(message);

            Console.ReadLine();
        }

        private static void TestBridge1()
        {
            var message = new EmailMessage
            {
                From = new EmailAddress { Address = "egy@teszt.hu", Display = "az elso cim" },
                To = new EmailAddress { Address = "ketto@teszt.hu", Display = "a masodik cim " },
                Subject = "tesztuzenet",
                Message = "Ez egy tesztuzenet, amit egy kuld a kettonek."
            };
            ///////////////////////////////////////////
            //Concrete Implementor
            SendWith strategy = SendWith.SendWithFactory();

            //abstraction
            var service = new EmailService(strategy);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////

            //Concrete Implementor
            SendWithExchange strategyMsx = SendWithExchange.SendWithExchangeFactory();

            //Nem kell uj service tipust peldanyositani!
            service = new EmailService(strategyMsx);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////

            //Concrete Implementor
            SendWithSendGrid strategySG = SendWithSendGrid.SendWithSendGridFactory();

            service = new EmailService(strategySG);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////

            //Concrete Implementor
            SendWithMandrill strategyM = SendWithMandrill.SendWithMandrillFactory();

            service = new EmailService(strategyM);
            service.Send(message);
            Console.WriteLine();
        }
        
    }
}
