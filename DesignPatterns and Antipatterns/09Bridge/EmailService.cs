using System;

namespace _09Bridge
{
    /// <summary>
    /// Az email kuldes alaposztalya, ebbol kiindulva keszitjuk el a specialis megoldasokat
    /// </summary>
    public class EmailService
    {      

        /// <summary>
        /// Ezt a fgv-t majd minden osztay felulirja a sajatjaval.
        /// </summary>
        /// <param name="message"></param>
        public virtual void Send(EmailMessage message)
        {
            Console.WriteLine("A kovetkezo uzenetet elkuldtuk a teszt szervizbol:");
            Console.WriteLine($"Kuldo: {message.From.Address}");
            Console.WriteLine($"Cimzett: {message.To.Address}");
            Console.WriteLine($"Targy: {message.Suject}");
            Console.WriteLine($"Uzenet: {message.Message}");
        }
    }
}