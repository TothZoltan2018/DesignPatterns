namespace _10Bridge1
{
    /// <summary>
    /// Dekorator minta pelda:
    /// Ha nem ferunk hozza az eredeti kodhoz, es KICSIT szeretnenk megvaltoztatni a mukodeset,
    /// akkor hasznaljuk. Az eredeti peldany Dependency Injection-nel megerkezik ide,
    /// meg is hivjuk, de a hivas elott elvegezhetunk kisebb dolgokat.
    /// 
    /// Tovabbi feltetel, hogy mi kezeljuk a felhasznalo oldali kodot, 
    /// igy a sajat feluletunk megjelenese nem okoz gondot.
    /// </summary>
    public class EmailServiceWithLogger : EmailService
    {
        private EmailService service;
        //private ISendWith sendWith;

        //Parameterkent fogadjuk az ososztalyat
        public EmailServiceWithLogger(EmailService service, AbstractSendWith sendWith) : base(sendWith)
        {
            this.service = service;
            //this.sendWith = sendWith; //ez nem kell, mert a sendWith-et atadtuk a bazisosztalynak
        }

        //valami celbol felulirjuk az ososztaly Send metodusat
        public new void Send(EmailMessage message)
        {
            //Itt tudjuk a  mukodest atirni
            System.Console.WriteLine("------> Levelkules eleje");//sajat kiegeszites
            service.Send(message);//az ososztaly metodusat hivjuk
            System.Console.WriteLine("------> Levelkules vege");//sajat kiegeszites
        }
    }
}
