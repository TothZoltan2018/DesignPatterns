using Ninject;

namespace _10Bridge1
{
    public class WelcomeMessageFactoryWithSendGrid : AbstractMessageFactory
    {
        private readonly StandardKernel kernel;

        //Az alabbiakat kell ebben az osztalyban legyartani:
        //var send = AbstractSendWith.Factory<SendWithExchange>();
        //var service = new EmailService(send);
        //var repo = kernel.Get<IPersonRepository>();
        //var template = new Templating();        

        public WelcomeMessageFactoryWithSendGrid()
        {
            //Fel kell parameterezni a Ninject-et:
            kernel = new StandardKernel(); //A Ninject osztalya
            kernel.Bind<IPersonRepository>()
                .To<PersonRepositoryTestData>() //Igy lehet valtani, hogy melyik repo-t hasznaljuk
                //.To<PersonRepositorySimpleData>() //Igy lehet valtani, hogy melyik repo-t hasznaljuk

                .InSingletonScope();//Csak egy peldanyban letezhet

        }

        public override IPersonRepository RepositoryFactory()
        {
            return kernel.Get<IPersonRepository>();
        }

        public override AbstractTemplating TemplateFactory()
        {
            return new WelcomeTemplating();
        }

        public override EmailService EmailServiceFactory()
        {
            var send = AbstractSendWith.Factory<SendWithSendGrid>();
            return new EmailService(send);
        }
    }
}