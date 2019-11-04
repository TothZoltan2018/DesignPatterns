using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03Iterator
{
    class Program
    {
        public static IEnumerable BejarhatoOsztaly { get; private set; }

        static void Main(string[] args)
        {
            foreach (var item in BejarhatoFuggveny())
            {
                Console.WriteLine($"Eredmeny: {item}");
            }

            var bejarhatoOsztaly = new BejarhatoOsztaly();

            bejarhatoOsztaly.Add("Elso elem");
            bejarhatoOsztaly.Add("Masodik elem");
            bejarhatoOsztaly.Add("Harmadik elem");
            bejarhatoOsztaly.Add("Negyedik elem");
            bejarhatoOsztaly.Add("Otodik elem");

            //ez egy kis csalas, mert System.Object jon a ciklusvaltozoba
            foreach (var item in bejarhatoOsztaly)
            {
                //de string eseten a ToString() mtodus eppen jot csinal,
                //igy ez elsore nem szur szemet
                Console.WriteLine($"ciklus: {item}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Ez egy ciklussal bejarhato fuggveny: Vagy visszater egy bejarhato
        /// osztalypeldannyal, vagy onmagaban tenyleg bejarhato.
        /// 
        /// A nevben szereplo ellentmondas feloldasat a fordito oldja meg, es 
        /// egy allapotgepet gyart a fuggvenyhivas moge, ami szimulalja  a bejarhato
        /// peldany mukodeset.
        /// </summary>
        /// <returns>vagy egy bejarhato objektum, vagy a fuggveny yield return-okkel
        /// szimulalja a bejarhatosagot</returns>
        private static IEnumerable<string> BejarhatoFuggveny()
        {
            yield return "egy";
            yield return "ketto";
            yield return "harom";
        }        
    }

    /// <summary>
    /// Ezen az osztalyon lehet ciklussal vegigmenni, vagyis bejarhato az osztaly
    /// </summary>
    class BejarhatoOsztaly : IEnumerable //,IEnumerator //Ezzel egy osztaly oldana meg az adattarolast es a bejarast
    {
        //kisebb xsalas kovetkezik, ugyanis a 
        //bejarhato oszralynak kell tartalom, amin vegig lehet menni
        List<string> list = new List<string>();

        //visszaadja a bejarot
        //Ehhez egy gyartofuggvenyt kell implementalni. Ennek lenyege, hogy nem a new operatorral peldanyositunk egy osztalyt, 
        //hanem keszitunk hozza egy fuggvenyt, ami megcsinalja a peldanyositast, parameterezest, es visszaadja az osztalypeldanyt.
        //Ez gyenge csatolast hoz letre (vs a new, ami eroset). Tehat a GetEnumerator eloallitja a Bejaro peldanyt.
        public IEnumerator GetEnumerator()
        {
            //ahhoz, hogy vegig tudjunk menni az adatokon, meg kell osztani azokat (bekuldjuk a lista refrenciajat a konstruktorba):
            //return new BejaroOsztaly(list);
            return new VisszafeleBejaroOsztaly(list);
        }

        internal void Add(string elem)
        {
            list.Add(elem);
        }
    }

    /// <summary>
    /// Bejaro definicio, ami a bejarhato osztaly egyes elemein fog vegiglepkedni.
    /// </summary>
    class BejaroOsztaly : IEnumerator
    {
        private List<string> list;
        private int position = -1; //meg nincs beallitva semmire

        public BejaroOsztaly(List<string> list)
        {
            this.list = list;
        }

        /// <summary>
        /// Leptet egyet a bejarando elemen es visszater a leptetes eredmenyevel
        /// </summary>
        /// <returns>true, ha a leptetes sikeres, false, ha nem</returns>
        public bool MoveNext()
        {
            position++;
            var erdemesUjrahivni = position < list.Count;
            Console.WriteLine($"    {nameof(BejaroOsztaly)}.{nameof(MoveNext)}: {position}, {erdemesUjrahivni}");
            return erdemesUjrahivni;
        }

        /// <summary>
        /// Sikeres leptetes utan hivhato. Visszater az aktualis elemmel
        /// </summary>
        public object Current
        {
            get
            {
                if (position == -1)
                {
                    throw new ArgumentOutOfRangeException("A bejarashoz eloszor leptetni kell!");
                }
                if (position > list.Count-1) //ures lista eseten is igaz: poz = 0 (mert leptettunk), es az eleszam 0
                {
                    throw new ArgumentOutOfRangeException("Tulmentunk a lehetseges elemeken!");
                }

                var current = list[position];
                Console.WriteLine($"    {nameof(BejaroOsztaly)}.{nameof(Current)}: {position}, {current}");
                return current;
            }
        }

        //a foreach ciklus ezt nem hivja meg
        /// <summary>
        /// Visszaallit mindent az elejere
        /// </summary>
        public void Reset()
        {           
            position = -1;
            Console.WriteLine($"    {nameof(BejaroOsztaly)}.{nameof(Reset)}");
        }
    }
    class VisszafeleBejaroOsztaly : IEnumerator
    {
        private List<string> list;
        private int position = -1; //meg nincs beallitva semmire

        public VisszafeleBejaroOsztaly(List<string> list)
        {
            list.OrderByDescending(x => x).ToList().Reverse(); //Nevsor szerint forditott sorrendben rendezes, utana az elemek indexe szerint visszafele                 
            this.list = list;
        }

        /// <summary>
        /// Leptet egyet a bejarando elemen es visszater a leptetes eredmenyevel
        /// </summary>
        /// <returns>true, ha a leptetes sikeres, false, ha nem</returns>
        public bool MoveNext()
        {
            position++;
            var erdemesUjrahivni = position < list.Count;
            Console.WriteLine($"    {nameof(VisszafeleBejaroOsztaly)}.{nameof(MoveNext)}: {position}, {erdemesUjrahivni}");
            return erdemesUjrahivni;
        }

        /// <summary>
        /// Sikeres leptetes utan hivhato. Visszater az aktualis elemmel
        /// </summary>
        public object Current
        {
            get
            {
                if (position == -1)
                {
                    throw new ArgumentOutOfRangeException("A bejarashoz eloszor leptetni kell!");
                }
                if (position > list.Count - 1) //ures lista eseten is igaz: poz = 0 (mert leptettunk), es az eleszam 0
                {
                    throw new ArgumentOutOfRangeException("Tulmentunk a lehetseges elemeken!");
                }

                var current = list[position];
                Console.WriteLine($"    {nameof(VisszafeleBejaroOsztaly)}.{nameof(Current)}: {position}, {current}");
                return current;
            }
        }

        //a foreach ciklus ezt nem hivja meg
        /// <summary>
        /// Visszaallit mindent az elejere
        /// </summary>
        public void Reset()
        {
            position = -1;
            Console.WriteLine($"    {nameof(VisszafeleBejaroOsztaly)}.{nameof(Reset)}");
        }
    }

}

