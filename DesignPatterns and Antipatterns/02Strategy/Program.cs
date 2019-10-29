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

            Console.ReadLine();
        }
    }
}
