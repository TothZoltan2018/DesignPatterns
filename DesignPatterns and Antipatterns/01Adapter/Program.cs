using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Adapter
{
    /// <summary>
    /// Feladat: Koruzenet kuldese az ugyfeleknek
    /// Ehhez rendelkezesre fog allni valamilyen adatbazis, 
    /// es valamilyen uzenetkuldo szolgaltatas (email, sms, chat)
    /// 
    /// Ezek kozul egyelore nem all rendelkezesre semmi, de kesobb illeszkednunk kell hozza
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {
            var example = new AdapterExample(new AddressTestRepository(), new MessageTestService());
            example.Start();
        }
    }
}
