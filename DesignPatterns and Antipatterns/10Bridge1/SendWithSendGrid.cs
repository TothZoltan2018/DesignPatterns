﻿using System;

namespace _10Bridge1
{
    /// <summary>
    /// Concrete Implementor
    /// </summary>
    public class SendWithSendGrid : AbstractSendWith
    {
        public string HostUrl { get; set; }
        public string ApiKey { get; set; }

        override public void Send(EmailMessage message)
        {
            {
                Console.WriteLine("A kovetkezo uzenetet elkuldtuk a SendGrid szervizbol API-val:");
                Console.WriteLine($"HostUrl: { HostUrl }");
                Console.WriteLine($"ApiKey: { ApiKey }");
                Console.WriteLine($"Kuldo: {message.From.Address}");
                Console.WriteLine($"Cimzett: {message.To.Address}");
                Console.WriteLine($"Targy: {message.Subject}");
                Console.WriteLine($"Uzenet: {message.Message}");
            }
        }

        protected override void Setup()
        {            
            HostUrl = "https://sendgrid.service.com";
            ApiKey = "SG-APIKEY";         
        }        
    }
}