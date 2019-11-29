namespace _10Bridge1
{
    internal class EmailServiceWithProxy : EmailService
    {
        private EmailService service;

        public EmailServiceWithProxy(EmailService service, AbstractSendWith sendWith) : base(sendWith)
        {
            this.service = service;
        }

        public new void Send(EmailMessage message)
        {
            //Itt tusunk jogosultsagot ellenorizni, vagy cach-t implementalni
            service.Send(message);
        }
    }
}