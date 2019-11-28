using System;

namespace _10Bridge1
{
    /// <summary>
    /// Concrete Implementor
    /// </summary>
    public class SendWithExchange : ISendWith
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public void Send(EmailMessage message)
        {
            Console.WriteLine("A kovetkezo uzenetet elkuldtuk az Exchange szervizbol SMTP-vel:");
            Console.WriteLine($"Host: { Host }, UserName: { UserName }");
            Console.WriteLine($"Kuldo: {message.From.Address}");
            Console.WriteLine($"Cimzett: {message.To.Address}");
            Console.WriteLine($"Targy: {message.Subject}");
            Console.WriteLine($"Uzenet: {message.Message}");
        }

        public static SendWithExchange SendWithExchangeFactory()
        {
            var strategyMsx = new SendWithExchange();
            strategyMsx.Host = "1.1.1.1";
            strategyMsx.UserName = "MSXUser";
            strategyMsx.Password = "MSXPassword";
            return strategyMsx;
        }
    }
}