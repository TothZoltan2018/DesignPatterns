using System;

namespace _10Bridge1
{
    public class MessageService
    {
        private EmailService service;
        private IPersonRepository repo;
        private AbstractTemplating template;
        private AbstractMessageFactory messageFactory;

        public MessageService(AbstractMessageFactory messageFactory)
        {
            this.messageFactory = messageFactory;
            this.service = messageFactory.EmailServiceFactory();
            this.repo = messageFactory.RepositoryFactory();
            this.template = messageFactory.TemplateFactory();
        }

        //public MessageService(EmailService service, IPersonRepository repo, Templating template)
        //{
        //    this.service = service;
        //    this.repo = repo;
        //    this.template = template;
        //}

        
        public void Run()
        {
            var persons = repo.GetPersonForMessages();
            foreach (var person in persons)
            {
                var message = template.GetMessageFor(person);
                service.Send(message);
                Console.WriteLine();
            }


        }
    }
}