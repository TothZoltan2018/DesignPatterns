﻿using System;
using System.Configuration;

namespace _10Bridge1
{
    /// <summary>
    /// Concrete Implementor
    /// </summary>
    public class SendWithExchange : AbstractSendWith
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        override public void Send(EmailMessage message)
        {
            Console.WriteLine("A kovetkezo uzenetet elkuldtuk az Exchange szervizbol SMTP-vel:");
            Console.WriteLine($"Host: { Host }, UserName: { UserName }");
            Console.WriteLine($"Kuldo: {message.From.Address}");
            Console.WriteLine($"Cimzett: {message.To.Address}");
            Console.WriteLine($"Targy: {message.Subject}");
            Console.WriteLine($"Uzenet: {message.Message}");
        }

        protected override void Setup()
        {            
            Host = ConfigurationManager.AppSettings[MagicValues.AppSettingsMsxHost];
            UserName = ConfigurationManager.AppSettings[MagicValues.AppSettingsMsxUserName];
            Password = ConfigurationManager.AppSettings[MagicValues.AppSettingsMsxPassword];
        }
    }
}