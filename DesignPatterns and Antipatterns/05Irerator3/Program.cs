using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05Irerator3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Antipattern: gyujtemneynek kozzetetele az osztalyunk feluleten
            var szamla = new Bankszamla();
            szamla.Atutalas(1, 200);
            szamla.Atutalas(2, 500);
            szamla.Atutalas(3, 100);
            szamla.Atutalas(4, 600);
                        
            foreach (var item in szamla.Atutalasok)
            {
                Console.WriteLine($"{item.Id}, Osszeg: {item.Osszeg}");
            }

            Console.WriteLine($"Egyenleg: {szamla.Egyenleg}");

            Console.WriteLine("\n--------- Torles ----------\n");

            //Hogy az alabbit vegre lehet hajtani, az nem jo, mert a torlest kovetoen inkonzisztens allapot lesz,
            //az Egyenleg marad az eredeti erteku (1400 - nem pedig 1200)
            ((List<Atutalas>)szamla.Atutalasok).Remove(((List<Atutalas>)szamla.Atutalasok)[0]); // --> exception lesz a ReadOnlyCollection miatt

            foreach (var item in szamla.Atutalasok)
            {
                Console.WriteLine($"{item.Id}, Osszeg: {item.Osszeg}");
            }

            Console.WriteLine($"Egyenleg: {szamla.Egyenleg}");



            Console.Read();

        }

        class Bankszamla
        {

            private List<Atutalas> atutalasok = new List<Atutalas>();

            //Ha listat kell kozzetennunk, hasznaljunk readonly feluletet
            public IEnumerable<Atutalas> Atutalasok
            { //a get csak azt garantalja, hogy a lista referenciajat nem tudom modositani, a tartalmat viszont igen
                get
                {
                    //return atutalasok.AsEnumerable(); //Ez nem oldja meg a problemat, a peldany marad, tehat visszaalakithato
                    return new ReadOnlyCollection<Atutalas>(atutalasok); //nem lehet modositani, a foprogramban nem lehet visszacastolni listava, a
                                                                         //ReadOnlyCollection-nak nincs Remove() metodusa --> exception
                }
            }

            public decimal Egyenleg { get; private set; } = 0;

            public void Atutalas(int id, decimal osszeg)
            {
                atutalasok.Add(new Atutalas(id, osszeg));
                Egyenleg += osszeg;
            }

        }

        class Atutalas
        {
            public Atutalas(int id, decimal osszeg)
            {
                Id = id;
                Osszeg = osszeg;
            }

            public int Id { get; }
            public decimal Osszeg { get; }
        }
    }
}
