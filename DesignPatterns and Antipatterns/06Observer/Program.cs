using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06Observer
{
    /// <summary>
    /// Keszitsubk egy olyan programot, ami hosszan dolgozik, es idorol idore szol, hogy hol tart.
    /// 
    /// Megfigyelo minta megjegyzesek:
    /// Amikor a megfigyelt ertesiti a megfigyeloket, akkor nem lehet tudni, hogy
    /// mekkora folyamat indul el. Ez teljesitmenyproblemakhoz vezethet.
    /// 
    /// Ahhoz, hogy kideruljon, mi valtozott, kulon megoldas kell.
    /// 
    /// A torlest implementalni kell
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var f = new FelhasznaloiFelulet();

            var n = new NaplozoModul();

            //var b = new BetoltoProgram(f, n);
            var b = new BetoltoProgram();
            b.Feliratkozas(f);
            b.Feliratkozas(n);

            b.Start();

            Console.WriteLine("Program vege");

            b.Leiratkozas(f);
            b.Leiratkozas(n);

            Console.ReadLine();
        }
    }

    public interface IUzenet
    {
        void Uzenet(IAllapot allapot);
    }

    public class NaplozoModul : IUzenet
    {
        public void Uzenet(IAllapot allapot)
        {
            Console.WriteLine($"NaplozoModul.Uzenet: {allapot.Allapot}");
        }
    }
 
    public class FelhasznaloiFelulet : IUzenet
    {
        public void Uzenet(IAllapot allapot)
        {
            Console.WriteLine($"FelhasznaloiFelulet.Uzenet: {allapot.Allapot}");
            //le kene kerdezni, hogy mi a helyzet
        }
    }

    public interface IAllapot 
    {
        int Allapot { get; }
    }
    
    class BetoltoProgram : IAllapot
    {
        private List<IUzenet> megfigyelok = new List<IUzenet>();

        //Ez tulsagosan statikus megoldas, ennel jobb lenne
        //dinamikusabban valtoztatni a kapcsolatokat
        //public BetoltoProgram(params IUzenet[] megfigyelok )
        //{
        //    //A parametertombben megkapott megfigyeloket felvesszuk az osztaly megfigyelok listajaba 
        //    foreach (var megfigyelo in megfigyelok)
        //    {
        //        this.megfigyelok.Add(megfigyelo);                
        //    }
        //}

        public void Feliratkozas(IUzenet megfigyelo)
        {
            if (!megfigyelok.Contains(megfigyelo))
            {
                megfigyelok.Add(megfigyelo);
            }
        }

        public void Leiratkozas(IUzenet megfigyelo)
        {
            if (!megfigyelok.Contains(megfigyelo))
            {
                megfigyelok.Remove(megfigyelo);
            }
        }


        internal void Start()
        {
            //csinal valami hosszabb folyamatot

            //20%
            Ertesites(20);

            //40%
            Ertesites(40);

            //70%
            Ertesites(70);

            //100%
            Ertesites(100);

            //VEGE
            //Ertesites();
        }

        private void Ertesites(int allapot)
        {
            this.allapot = allapot;
            foreach (var megfigyelo in megfigyelok)
            {
                megfigyelo.Uzenet(this);
            }
        }

        private int allapot;
        public int Allapot { get { return allapot; } }
    }
}
