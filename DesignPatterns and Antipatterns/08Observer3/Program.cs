using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08Observer3
{
    class Program
    {
        /// <summary>
        /// A feladat ugyanaz, mint eddig, csak most esemenyekkel
        /// 
        /// Az allapot valtozasat a megfigyelt kozolheti DTO-n keresztul,
        /// de a teljes megfigyelt peldanyt elkuldjuk a parameterlistaban, igy
        /// a megfigyelt mindenhez hozzafer
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var f = new FelhasznaloiFelulet();

            var n = new NaplozoModul();

            var b = new BetoltoProgram();

            //Feliratkozasok
            //a)
            //b.AllapotValtozasTortent += B_AllapotValtozasTortent;

            //b) Anonymus delegate-tel
            //Ha ezzel a lambdas megoldassal iratkozok fel fuggvenyeket, akkor olyan fgv szignaturat is megadhatunk, ami elter a listaetol.
            //b.AllapotValtozasTortent += (object o, AllapotUzenet e) => { f.Uzenet(o); }; // Csak egy parameter van.
            //b.AllapotValtozasTortent += (object o, AllapotUzenet e) => { n.Uzenet(o, e.Allapot); }; //Nem az egesz osztaly, hanem csak az Allapot property-t adom at

            b.AllapotValtozasTortent += f.Uzenet;
            b.AllapotValtozasTortent += n.Uzenet;

            b.Start();

            b.AllapotValtozasTortent -= f.Uzenet;
            b.AllapotValtozasTortent -= n.Uzenet;
        }

        //Atlagos esemenyvezerlo: nekunk most nem ez kell
        //private static void B_AllapotValtozasTortent(object sender, AllapotUzenet e)
        //{
        //    throw new NotImplementedException();
        //}
    }

    internal class BetoltoProgram        
    {
        //kell egy DTO-t gyartani amit le kell szarmaztatni az EventArgs osztalybol: AllapotUzenet
        //Ket dolgot keszitettunk:
            //1. Hivaslista, amit a .Net implemental nekunk. Erre a fuggvenyeknek fel lehet iratkozni. Mindegyik meg lesz hivva, de a sorrend nem garantalt.

        //Az esemenyt a BetoltoProgram osztalyon kivulrol nem lehet meghivni.
        //Nem adhato neki ertek az osztalyon kivulrol.
        //Csak le- es feliratkozni lehet ra az osztalyon kivulrol.
        public event EventHandler<AllapotUzenet> AllapotValtozasTortent;

        //Az esemeny hasznalata. Nevkonvencio: OnEsemenyNeve
        private void OnAllapotValtozasTortent(int allapot)
        {
            //Ha nem mentenem el az esemenyek hivaslistajat, akkor a menetkozben fel/leiratkozo fgv-ek gondot okozhatnanak
            //A hivaslistan szereplo osszes fgv-t meghivjuk szepen egymas utan
            AllapotValtozasTortent?.Invoke(this, new AllapotUzenet(allapot: allapot)); //mindig azt az objektumot irjuk az elso parameternek, ami kuldi az esemenyt

        }

        internal void Start()
        {
            //csinal valami hosszabb folyamatot

            //20%
            Ertesites(20);

            //40%
            Ertesites(40);

            //Hiba(new Exception("Itt valami hiba tortent"));
            //70%
            Ertesites(70);

            //100%
            Ertesites(100);

            //VEGE
            //Vege();
        }

        private void Ertesites(int allapot)
        {
            OnAllapotValtozasTortent(allapot);
        }

        private void Vege()
        {
            throw new NotImplementedException();
        }

        private void Hiba(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// O egy DTO, csak adattagok vannak benne.
    /// .Net konvencio: Az esemeny argumentumakent szereplo DTO-kat le kell
    /// szarmaztatni az EventArgs osztalybol.( Bar nelkule is mukodne)
    /// </summary>
    public class AllapotUzenet : EventArgs
    {
        private int allapot;
        public int Allapot { get { return allapot; } }

        public AllapotUzenet(int allapot)
        {
            this.allapot = allapot;
        }
    }

    internal class NaplozoModul
    {
        internal void Uzenet(object sender, AllapotUzenet e)
        {
            Console.WriteLine($"NaplozoModul: {e.Allapot}");           
        }
    }

    internal class FelhasznaloiFelulet
    {
        internal void Uzenet(object o, AllapotUzenet e)
        {
            Console.WriteLine($"FelhasznaloiFelulet: {e.Allapot}");            
        }
    }
}
