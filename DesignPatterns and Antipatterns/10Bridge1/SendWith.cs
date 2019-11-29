using System;

namespace _10Bridge1
{
    /// <summary>
    /// Concrete Implementor
    /// </summary>
    public class SendWith : AbstractSendWith
    {
        override public void Send(EmailMessage message)
        {
            Console.WriteLine("A kovetkezo uzenetet elkuldtuk a teszt szervizbol:");
            Console.WriteLine($"Kuldo: {message.From.Address}");
            Console.WriteLine($"Cimzett: {message.To.Address}");
            Console.WriteLine($"Targy: {message.Subject}");
            Console.WriteLine($"Uzenet: {message.Message}");
        }

        protected override void Setup()
        {
        }
    }
}