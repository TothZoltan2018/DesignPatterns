using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace _01Adapter
{
    public class AdapterExample
    {
        public void Start()
        {
            //1. Legyen egy adatforrasunk
            /////////////////////////////
            //az alabbi nem jo, mert eros csatolast csinal:
            //var list = new List<string> { "tz@gmail.com" };
            //Helyette:
            var repo = new AddressRepository(); //Az ilyen, adatkoat szolgaltato osztalyt nonvencionalisan Repository-nak hivjak

            //2. Legyen egy email megoldasunk (uzenetkuldo megoldas)
            /////////////////////////////////////////////////////

            //Pl.: smtp. Lassu, tobb sec is lehet egyetlen level elkuldese.
            //var message = new MailMessage;
            //message.To.Add(new MailAddress("email cim"));
            //stb.
            //smtp helyett skkal jobb pl. a sendgrid vagy mailchimp api-jat hasznalni.
            //de lehet, hogy sms-t akarunk msjd kesobb hasznalni stb...

            //A kod ujrafelhasznalhatosaganak feltetele es biztositeka:
            //High cohesion - low coupling

            //Csatolas: ket pnjeltum akkor van csatolasban, ha ay egyik modosulasa NEM zarja 
            //ki a masik megvaltozasat
            //minnel erosebb a csatolas, annal valoszinubb, hogy meg is valtozik a masik objektum

            //Az eros csatolas ellen mit lehet tenni? Indirekcio.                
            // [egyik obj.] ----- (csatolas) ----- [koztes obj.] ----- (csatolas) -----  [masik obj.]
            //Igy mar a ket szelso objektum nincs csatolasban egymassal.

            //Indirekcio: keszitunk egy koztes osztalyt
            var messageService = new MessageService();

            //es ezeket kossuk ossze
            var addressList = repo.GetAddresses();
            foreach (var address in addressList)
            {
                messageService.AddMessage(to: address.Email, subject: "Uzenet cime", text: "Szoveg");
            }

            

            messageService.SendMessages();

        }
    }
}