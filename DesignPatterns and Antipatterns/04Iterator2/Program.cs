using System;
using System.Collections;
using System.Collections.Generic;

namespace _04Iterator2
{
    class Program
    {
        static void Main(string[] args)
        {
            var bejarhatoOsztaly = new BejarhatoOsztaly();
            bejarhatoOsztaly.Add(new SajatOsztaly("Elso bejegyzes"));
            bejarhatoOsztaly.Add(new SajatOsztaly("Masodik bejegyzes"));
            bejarhatoOsztaly.Add(new SajatOsztaly("Harmadik bejegyzes"));
            bejarhatoOsztaly.Add(new SajatOsztaly("Negyedik bejegyzes"));

            foreach (var item in bejarhatoOsztaly)
            {
                Console.WriteLine(item.Uzenet);
                //Ha tudom, hogy az item SajatOsztaly tipusu, csak akkor tudok castolas utan
                //(elkerem a SajatOsztaly feluletet) a property-kre hivatkozni

                //generikus IEnumarable eseten nincs szukseg a cast-ra
                // if (((SajatOsztaly)item).Created.DayOfWeek == DayOfWeek.Friday)
                //  {}
            }
            //Miutan a foreach veget er, megivja a Dispose() fgvt.

            //A foreach a kovetkezo mechanizmust hivja eletre:            
            //using (var bejaro = bejarhatoOsztaly.GetEnumerator())//a bejaro IDisposable, ezert using blokkban kell hasznalni
            var bejaro = bejarhatoOsztaly.GetEnumerator();
            try
            {
                var leszKovetkezo = true;
                do
                {
                    leszKovetkezo = bejaro.MoveNext();
                    var item = bejaro.Current;
                    {
                        //Ez itt a foreach ciklus belseje      
                        Console.WriteLine(item.Uzenet);
                    }
                } while (leszKovetkezo);
            }
            finally
            {
                if (bejaro!=null)
                {
                    ((IDisposable)bejaro).Dispose();
                }                
            }

            Console.ReadLine();
        }
    }
    class SajatOsztaly
    {
        public SajatOsztaly(string uzenet)
        {
            Uzenet = uzenet;
        }

        public string Uzenet { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }

        //A Console WrieLine a hatterben meghivja a ToString metodust, ami alapbol nem mukodik megfeleloen, ezert felulirjuk
        //es az uzenet property-t adja vissza.
        //public override string ToString()
        //{
        //    return Uzenet;
        //}
    }

    class BejarhatoOsztaly : IEnumerable<SajatOsztaly>
    {
        List<SajatOsztaly> list = new List<SajatOsztaly>();

        internal void Add(SajatOsztaly sajatOsztaly)
        {
            list.Add(sajatOsztaly);
        }

        public IEnumerator<SajatOsztaly> GetEnumerator()
        {
            return new BejaroOsztaly(list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {//ez egyszeru duplikalas, csak meghivom a tipusos fuggvenyt
            return this.GetEnumerator();
        }
    }

    class BejaroOsztaly : IEnumerator<SajatOsztaly>
    {
        private List<SajatOsztaly> list;
        private int position = -1;

        public BejaroOsztaly(List<SajatOsztaly> list)
        {
            this.list = list;
        }

        public SajatOsztaly Current
        {
            get { return list[position]; }
        }

        //ez egyszeru duplikalas, csak meghivom a tipusos fuggvenyt
        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public bool MoveNext()
        {
            return ++position < list.Count;
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose() { } //nincs tennivalonk, nem csinal a Dispose() semmit

        //Az IDisposable felulet miatt ez kotelezo, ha a Dispose valamit csinal
        //Kotelezo a finalizer. (A .Net automatikusan hivja, ha mar nem kell az objektum)
        //~BejaroOsztaly()
        //{
        //    Dispose(false);
        //}

        //private void Dispose(bool isManagedDispose)
        //{
        //    if (isManagedDispose)
        //    {
        //        //Itt takaritjuk a menedzselt eroforrasokat
        //        if (list != null)
        //        {
        //            list = null; //Megszuntejuk a referenciat
        //        }
        //    }

        //    //A nem menedzselt eroforrasok takaritasa
        //}

        //public void Dispose()
        //{
        //    Dispose(true);

        //    //Mivel takaritottunk, a finalizernek mar nincs funkcioja, jelezzuk, hogy nem kell
        //    GC.SuppressFinalize(this);
        //}
    }
}
