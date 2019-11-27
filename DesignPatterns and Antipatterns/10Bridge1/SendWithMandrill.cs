using System;

namespace _10Bridge1
{
    internal class SendWithMandrill : ISendWith
    {
        public string HostUrl { get; set; }
        public string ClientSecret { get; set; }
        public string ClientKey { get; set; }

        public void Send(EmailMessage message)
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