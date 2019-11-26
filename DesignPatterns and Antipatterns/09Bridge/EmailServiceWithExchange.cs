using System;

namespace _09Bridge
{
    public class EmailServiceWithExchange : EmailService
    {
        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public override void Send(EmailMessage message)
        {
            Console.WriteLine("A kovetkezo uzenetet elkuldtuk az Exchange szervizbol SMTP-vel:");
            Console.WriteLine($"Host: { Host }, UserName: { UserName }");
            Console.WriteLine($"Kuldo: {message.From.Address}");
            Console.WriteLine($"Cimzett: {message.To.Address}");
            Console.WriteLine($"Targy: {message.Suject}");
            Console.WriteLine($"Uzenet: {message.Message}");
        }
    }
}