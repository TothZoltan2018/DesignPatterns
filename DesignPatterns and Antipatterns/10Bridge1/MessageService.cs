using System;

namespace _10Bridge1
{
    public class MessageService
    {
        private EmailService service;
        private IPersonRepository repo;
        private Templating template;
        private Func<Person> p;
        private Person person;

        public MessageService(EmailService service, IPersonRepository repo, Templating template)
        {
            this.service = service;
            this.repo = repo;
            this.template = template;
        }

        
        public void Run()
        {
            var person = repo.GetBirthdayPersons();
            var message = template.GetMessageFor(person);
            service.Send(message);
        }
    }
}