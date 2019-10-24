using System;
using System.Collections.Generic;

namespace _01Adapter
{
    public class MessageService
    {
        //Ne hasznalj null-t: null object pattern
        private List<Message> messages = new List<Message>();

        public MessageService()
        {
        }

        public void AddMessage(string to, string subject, string text)
        {
            messages.Add(new Message { To = to, Subject = subject, Text = text });
        }

        public void SendMessages()
        {
            foreach (var message in messages)
            {
                Console.WriteLine($"To = {message.To}, Subject = {message.Subject}, Text = {message.Text}");
            }
        }
    }
}