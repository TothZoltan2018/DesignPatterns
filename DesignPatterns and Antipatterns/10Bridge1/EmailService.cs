using System;

namespace _10Bridge1
{/// <summary>
/// Fogadja a strategiat (a Send metodust, es meghivja)
/// </summary>
    public class EmailService
    {
        private AbstractSendWith strategy;

        public EmailService(AbstractSendWith strategy)
        {
            this.strategy = strategy;
        }

        public void Send(EmailMessage message)
        {
            strategy.Send(message);
        }
    }
}