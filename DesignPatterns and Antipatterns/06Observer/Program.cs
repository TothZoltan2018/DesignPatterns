using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06Observer
{
    /// <summary>
    /// Keszitsubk egy olyan programot, ami hosszan dolgozik, es idorol idore szol, hogy hol tart.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var f = new FelhasznaloiFelulet();

            var n = new NanplozoModul();

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
        void Uzenet(int allapot);
    }

    public class NanplozoModul : IUzenet
    {
        public void Uzenet(int allapot)
        {
            Console.WriteLine($"NaplozoModul.Uzenet: {allapot}");
        }
    }
 
    public class FelhasznaloiFelulet : IUzenet
    {
        public void Uzenet(int allapot)
        {
            Console.WriteLine($"FelhasznaloiFelulet.Uzenet: {allapot}");
            //le kene kerdezni, hogy mi a helyzet
        }
    }

    class BetoltoProgram
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
                megfigyelo.Uzenet(allapot);
            }
        }

        private int allapot;
        public int Allapot { get { return allapot; } }
    }
}
