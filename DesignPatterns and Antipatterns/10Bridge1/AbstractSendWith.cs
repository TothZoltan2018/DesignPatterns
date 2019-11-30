using System;
namespace _10Bridge1
{
    /// <summary>
    /// Az absztrak osztaly a kovetkezoket tudja:
    /// 1. Lehetove teszi, hogy a leszarmaztatott osztalyok peldanyositasa ezen az egy helzen tortenjek.
    /// 2. Eloirja, hogy minden leszarmaztatott osztalynak legyen Send() es Setup() fuggvenye
    /// 3. A Setup() fuggvenyt a konstruktora meghivja --> a leszarmazott osztalyok peldanyosodasakor
    ///     meghivodik a leszarmazott osztaly Setup() fuggvenye
    /// </summary>
    public abstract class AbstractSendWith
    {
        public AbstractSendWith()
        {
            Setup(); //Mindig az adott leszarmaztatas impelemntaciojat hivja
        }

        abstract protected void Setup();

        abstract public void Send(EmailMessage message);

        /// <summary>
        /// protected: kivulrol nem hivhato a Setup fgv, 
        /// csak a hierarchian belulrol. Celja a peldanyositas
        /// utan a Setup megivasa, a parameterek eretkadasanak celjabol
        /// </summary>
 
        public static T Factory<T>()
            where T: AbstractSendWith, new() //A constraint megadja, hogy milyen Tipust fogadhat, es annak peldanyosithatonak kell lennie
        {
            //A felparameterezes az adott implementacio dolga, ezert nem itt vegezzuk, hanem a AbstractSendWith() konstruktorban
            //var tmp = new T();
            //tmp.Setup();

            //Mivel egy helyen peldanyositunk, ezert
            //tudjuk parameterezni a peldanyositast
            //Debughoz a felparameterezett Test valtozat, 
            //Release-hez a parametereket az appconfig-bol szedo lesz kivalasztva
#if DEBUG
            if (typeof(T) == typeof(SendWithExchange))
            {
                return (T)(AbstractSendWith)(new SendWithExchangeTest());
            }
#endif
            return new T();
        }

        /// <summary>
        /// Ugyanazt tudja, mint a fenti Factory fgv, de nem elegans es nem generikus. Nem a legjobb megoldas.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AbstractSendWith Factory(SendWithTypes type)
        {
            switch (type)
            {
                case SendWithTypes.SendWith:
                    return new SendWith();
                case SendWithTypes.SendWithSendGrid:
                    //1. A felparameterezes az adott implementacio dolga
                    //2. Innen is hvhatjuk a Setup fgvt, de a konstruktorbol
                    //   mar kozpontositva van, az elegansabb.
                    //var tmp = new SendWithSendGrid();
                    //tmp.Setup();
                    return new SendWithSendGrid();
                case SendWithTypes.SendWithExchange:
                    //Ugyanaz, mint eggyel feljebb
                    return new SendWithExchangeTest();
                case SendWithTypes.SendWithMandrill:
                    //Ugyanaz, mint eggyel feljebb
                    return new SendWithMandrill();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

    }

    public enum SendWithTypes //Csak a "switch-es" Factory-hoz kell
    {
        SendWith, SendWithSendGrid, SendWithExchange, SendWithMandrill
    }
}