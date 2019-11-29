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
            TestBridge1();

            //A decorator/proxy/facade
            //TestBridgeDecoratorAndProxy();

            Console.ReadLine();
        }

        private static void TestBridgeDecoratorAndProxy()
        {
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

            //var sendWith = AbstractSendWith.Factory(SendWithTypes.SendWith); //statikus fgv-t keszitettunk
            var sendWith = AbstractSendWith.Factory<SendWith>();

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
            //var strategy = AbstractSendWith.Factory(SendWithTypes.SendWith);
            var strategy = AbstractSendWith.Factory<SendWith>(); //statikus fgv-t keszitettunk

            //abstraction
            var service = new EmailService(strategy);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////

            //Concrete Implementor
            //var strategyMsx = AbstractSendWith.Factory(SendWithTypes.SendWithExchange);
            var strategyMsx = AbstractSendWith.Factory<SendWithExchange>();

            //Nem kell uj service tipust peldanyositani!
            service = new EmailService(strategyMsx);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////

            //Concrete Implementor
            //var strategySG = AbstractSendWith.Factory(SendWithTypes.SendWithSendGrid);
            var strategySG = AbstractSendWith.Factory<SendWithSendGrid>();

            service = new EmailService(strategySG);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////

            //Concrete Implementor
            //var strategyM = AbstractSendWith.Factory(SendWithTypes.SendWithMandrill);
            var strategyM = AbstractSendWith.Factory<SendWithMandrill>();

            service = new EmailService(strategyM);
            service.Send(message);
            Console.WriteLine();
        }
        
    }
}

