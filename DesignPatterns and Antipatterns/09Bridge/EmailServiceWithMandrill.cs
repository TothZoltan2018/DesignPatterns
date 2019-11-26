using System;

namespace _09Bridge
{
    public class EmailServiceWithMandrill : EmailService
    {

        public string ClientSecret { get; internal set; }
        public string HostUrl { get; internal set; }
        public string ClientKey { get; internal set; }

        public override void Send(EmailMessage message)
        {
            Console.WriteLine("A kovetkezo uzenetet elkuldtuk a Mandrill szervizbol API-val:");
            Console.WriteLine($"HostUrl: { HostUrl }");
            Console.WriteLine($"ClientSecret: { ClientSecret  }");
            Console.WriteLine($"ClientKey: { ClientKey }");
            Console.WriteLine($"Kuldo: {message.From.Address}");
            Console.WriteLine($"Cimzett: {message.To.Address}");
            Console.WriteLine($"Targy: {message.Suject}");
            Console.WriteLine($"Uzenet: {message.Message}");

        }
    }
}