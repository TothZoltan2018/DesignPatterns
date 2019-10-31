using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02Strategy
{
    /// <summary>
    /// A feladat: Kulonbozo algoritmusok beepotese a rendszerbe
    /// - Kerjuk le, hogy hany email-t kuldtunk mar eddig
    /// - Kerjuk le, hogy atlagosan hany email-t kuldtunk egy-egy cimzettnek
    /// - email kuldes csak egy meghatarozott csoportnak
    /// - az uzenet szovege legyen:
    ///         - plain text
    ///         - html 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {            
            var service = new DataService(new AddressStrategyTestRepo());
            #region nem jo megoldasok
            // - Kerjuk le, hogy hany email-t kuldtunk mar eddig
            var count = service.GetSumEmailCount();
            Console.WriteLine($"Email-ek szama: {count}");

            // - Kerjuk le, hogy atlagosan hany email-t kuldtunk egy-egy cimzettnek
            var avg = service.GetAvgEmailCount();
            Console.WriteLine($"Atlagos email-ek szama: {avg}");

            Console.WriteLine(); Console.WriteLine();

            count = service.Report(ReportType.Sum);
            Console.WriteLine($"Email-ek szama: {count}");

            avg = service.Report(ReportType.Average);
            Console.WriteLine($"Atlagos email-ek szama: {avg}");

            Console.WriteLine(); Console.WriteLine();

            var service2 = new DataService2(new AddressStrategyTestRepo());
            count = service2.Report(ReportType.Average); //A ReportType-ot nem hasznalja az override-olt Report fgv (mindegy, hogy Sum vag Average)
            Console.WriteLine($"VIP email-ek szama: {count}");
            Console.WriteLine(); Console.WriteLine();
            #endregion
            //###################### Jo megoldasok ######################
            //###################### 1. Strategia minta ######################
            var service3 = new DataService(new AddressStrategyTestRepo(), new SumStrategy());
            //lehet ilyet is csinalni, csak a null ertekere figyelni kell
            //service3.SetStrategy(new SumStrategy());

            count = service3.ReportWithStrategy();
            Console.WriteLine($"Email-ek szama - strategiaval: {count}");
            Console.WriteLine();

            //Ilyet is lehetne implementalni, szinten a null ertekere figyelni kell
            //avg = service3.ReportWithStrategy(new AvgStrategy());
            var service4 = new DataService(new AddressStrategyTestRepo(), new AvgStrategy());

            Console.WriteLine($"Email-ek atlagaos szama - strategiaval: {service4.ReportWithStrategy()}");
            Console.WriteLine();

            //#################### 2. A .NET megoldasa: delegate ########################
                        
            Console.WriteLine();
            //A WriteLine ujabb formatumaval ( $"...") nem tudtam megadni... Talan nem szereti a sortorest.
            Console.WriteLine("Email-ek szama - delegate-tel: {0}", service.ReportWithDelegate(
                list => list.Sum(x => x.EmailCount) //Ez a strategiat leiro delegate
            )); 

            
            Console.WriteLine("Email-ek atlagos szama - delegate-tel: {0}", service.ReportWithDelegate(
                list => //Ez a strategia mintat leiro delegate
                {
                   avg = (int)Math.Round(list.Average(x => x.EmailCount));
                   return avg;
                }
            ));
            
            Console.WriteLine();




            Console.ReadLine();
        }
    }
}
