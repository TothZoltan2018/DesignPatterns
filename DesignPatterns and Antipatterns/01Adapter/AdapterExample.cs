using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace _01Adapter
{
    public class AdapterExample
    {
        private readonly IAddressRepository repository;
        private readonly IMessageService service;

        public AdapterExample(IAddressRepository repository, IMessageService service)
        {
            //if (repository == null) { throw new ArgumentException(nameof(repository)) }
            //this.repository = repository == null ? throw new ArgumentException(nameof(repository)) : repository;
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            

        }

        public void Start()
        {
            //1. Legyen egy adatforrasunk
            /////////////////////////////
            //az alabbi nem jo, mert eros csatolast csinal:
            //var list = new List<string> { "tz@gmail.com" };
            //Az adatokat kszolgaltato osztaly neve: repository.
            //Viszont mar kivulrol megkapom Dependency Injection-nel (IAddressRepository repository), ezert nem kell osztalypeldanyt letrehoznunk
            //Azaz a fuggosegeimet kivulrol kapom neg, nem en allitom elo. Ezert nem kell az alabbi sor:
            //var repo = new AddressRepository(); //Az ilyen, adatokat szolgaltato osztalyt konvencionalisan Repository-nak hivjak


            //2. Legyen egy email megoldasunk (uzenetkuldo megoldas)
            /////////////////////////////////////////////////////

            //Pl.: smtp. Lassu, tobb sec is lehet egyetlen level elkuldese.
            //var message = new MailMessage;
            //message.To.Add(new MailAddress("email cim"));
            //stb.
            //smtp helyett sokkal jobb pl. a sendgrid vagy mailchimp api-jat hasznalni.
            //de lehet, hogy sms-t akarunk majd kesobb hasznalni stb...

            //##########################################################
            //A kod ujrafelhasznalhatosaganak feltetele es biztositeka:
            //High cohesion - low coupling
            //##########################################################

            //Csatolas: ket objektum akkor van csatolasban, ha az egyik modosulasa NEM zarja 
            //ki a masik megvaltozasat
            //minnel erosebb a csatolas, annal valoszinubb, hogy meg is valtozik a masik objektum

            //Az eros csatolas ellen mit lehet tenni? Indirekcio.                
            // [egyik obj.] ----- (csatolas) ----- [koztes obj.] ----- (csatolas) -----  [masik obj.]
            //Igy mar a ket szelso objektum nincs csatolasban egymassal.

            //Indirekcio: keszitunk egy koztes osztalyt
            //Viszont mar kivulrol megkapom Dependency Injection-nel (IMessageService service). Ezert nem kell az alabbi sor:
            //var messageService = new MessageService();

            //es ezeket kossuk ossze
            var addressList = repository.GetAddresses();

            foreach (var address in addressList)
            {
                service.AddMessage(to: address.Email, subject: "Uzenet cime", text: "Szoveg");
            }

            

            service.SendMessages();

        }
    }
}