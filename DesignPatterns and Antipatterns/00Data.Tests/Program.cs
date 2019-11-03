using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00Data.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new AddressContext();
            while (!Console.KeyAvailable)
            {                
                foreach (var addr in db.Myaddresses.ToList()) //ToList: igy csak 1x fordul az adatbazishoz, utana a memoriabol dolgozik minden iteracioban
                {
                    Console.WriteLine(addr.Address);
                }
            }

            Console.ReadLine();
        }
    }
}
