using System;

namespace _09Bridge
{
    public class EmailServiceWithSendGrid : EmailService
    {
        public string ApiKey { get; set; }
        public string HostUrl { get; internal set; }

        public override void Send(EmailMessage message)
        {
            Console.WriteLine("A kovetkezo uzenetet elkuldtuk a SendGrid szervizbol API-val:");            
            Console.WriteLine($"HostUrl: { HostUrl }");
            Console.WriteLine($"ApiKey: { ApiKey }");
            Console.WriteLine($"Kuldo: {message.From.Address}");
            Console.WriteLine($"Cimzett: {message.To.Address}");
            Console.WriteLine($"Targy: {message.Suject}");
            Console.WriteLine($"Uzenet: {message.Message}");
        }

    }
}