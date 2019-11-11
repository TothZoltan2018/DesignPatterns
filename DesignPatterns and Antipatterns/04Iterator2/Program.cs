using System;
using System.Collections;
using System.Collections.Generic;

namespace _04Iterator2
{
    class Program
    {
        static void Main(string[] args)
        {
            var bejarhatoOsztaly = new BejarhatoOsztaly<SajatOsztaly>();
            bejarhatoOsztaly.Add(new SajatOsztaly("Elso bejegyzes"));
            bejarhatoOsztaly.Add(new SajatOsztaly("Masodik bejegyzes"));
            bejarhatoOsztaly.Add(new SajatOsztaly("Harmadik bejegyzes"));
            bejarhatoOsztaly.Add(new SajatOsztaly("Negyedik bejegyzes"));

            foreach (var item in bejarhatoOsztaly)
            {                
                Console.WriteLine(item.Uzenet);
                //A bejaro nem teljes gyujtemenyeleres, ezert iyet nem tehetunk:                
                //bejarhatoOsztaly.Remove(item);
                //De ha megis:
                bejarhatoOsztaly.Remove(item);
                //tudnom kell arrol, hogy ha az adatokat modositjak,
                //nem ertesulok rola, es csak akkor tudok ellene tenni, 
                //ha erre a forgatokonyvre felkeszulok:

                //1. Nem teszunk listat a kirakatba (nem tesszuk elerhetove a lista adatainkat kivulrol)
                //2. Igy ertesulunk a modositasrol, es kezelni tudjuk
                //3. Helyzettol fugg, hogy engedjuk -e a modositast, es ertesitjuk a bejaro(ka)t, vagy mar a modositast sem engedjuk
                //4. Parhuzamos bejarasokra a sajat bejaroval lehet a legegyszerubben felkeszulni
                //5. Ha a bejaro es a bejarhato egy osztalyba kerul, akkor a dispose() implaemntacioja tul bonyolult lehet 

            }
            //Miutan a foreach veget er, megivja a Dispose() fgvt.

            //A foreach a kovetkezo mechanizmust hivja eletre:            
            //using (var bejaro = bejarhatoOsztaly.GetEnumerator())//a bejaro IDisposable, ezert using blokkban kell hasznalni
            //var bejaro = bejarhatoOsztaly.GetEnumerator();
            //try
            //{
            //    var leszKovetkezo = true;
            //    do
            //    {
            //        leszKovetkezo = bejaro.MoveNext();
            //        var item = bejaro.Current;
            //        {
            //            //Ez itt a foreach ciklus belseje      
            //            Console.WriteLine(item.Uzenet);
            //        }
            //    } while (leszKovetkezo);
            //}
            //finally
            //{
            //    if (bejaro!=null)
            //    {
            //        ((IDisposable)bejaro).Dispose();
            //    }                
            //}


            ////Nagyon fontos annak kezelese, hogy a bejarhato peldeny elemei modosultak -e?
            ////Mert ha igen, akkor valamit tenni kell. A List<T> Enumerator-a peldaul exception-t dob
            //var lista = new List<string>(new string[] { "egy", "ketto", "harom" });
            ////var lista = new List<string>() { "egy", "ketto", "harom" }; //ugyanaz, mint elobb

            //foreach (var item in lista)
            //{
            //    Console.WriteLine($"elem: {item}");
            //    lista.Remove(item); //-->
            //--> System.InvalidOperationException: 'Collection was modified; enumeration operation may not execute.'

            //}

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

    class BejarhatoOsztaly<T> : IEnumerable<T>
    {
        //Antipattern pelda: listat nem osztunk meg!
        List<T> list = new List<T>();

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Remove(T item)
        {
            list.Remove(item);
            //ertesitenem kell a bejaroimat, hogy helyzet van!!!
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BejaroOsztaly<T>(list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {//ez egyszeru duplikalas, csak meghivom a tipusos fuggvenyt
            return this.GetEnumerator();
        }

    }

    class BejaroOsztaly<T> : IEnumerator<T>
    {
        private List<T> list;
        private int position = -1;

        public BejaroOsztaly(List<T> list)
        {
            this.list = list;
        }

        public T Current
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
