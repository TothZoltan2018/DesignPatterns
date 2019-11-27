using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10Bridge1
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = new EmailMessage
            {
                From = new EmailAddress { Address = "egy@teszt.hu", Display = "az elso cim" },
                To = new EmailAddress { Address = "ketto@teszt.hu", Display = "a masodik cim " },
                Suject = "tesztuzenet",
                Message = "Ez egy tesztuzenet, amit egy kuld a kettonek."
            };

            ///////////////////////////////////////////
            //Implementor
            var strategy = new SendWith();

            //Absztrakcio
            var service = new EmailService(strategy);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////
            //Konkret implementator
            var strategyMsx = new SendWithExchange();
            strategyMsx.Host = "1.1.1.1";
            strategyMsx.UserName = "MSXUser";
            strategyMsx.Password = "MSXPassword";

            //Nem kell uj service tipust peldanyositani!
            service = new EmailService(strategyMsx);
            service.Send(message);
            Console.WriteLine();
            ///////////////////////////////////////////

            var strategySG = new SendWithSendGrid();
            strategySG.HostUrl = "https://sendgrid.service.com";
            strategySG.ApiKey = "SG-APIKEY";

            service = new EmailService(strategySG);
            service.Send(message);            
            Console.WriteLine();
            ///////////////////////////////////////////
            
            var strategyM = new SendWithMandrill();
            strategyM.HostUrl = "https://api.mandrill.com";
            strategyM.ClientSecret = "MANDRILL-SECRET";
            strategyM.ClientKey = "MANDRILL-KEY";

            service = new EmailService(strategyM);
            service.Send(message);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
