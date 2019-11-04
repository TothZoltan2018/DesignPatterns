using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine(item);
                //Ha tudom, hogy az item SajatOsztaly tipusu, csak akkor tudok castolas utan
                //(elkerem a SajatOsztaly feluletet) a property-kre hivatkozni
                if (((SajatOsztaly)item).Created.DayOfWeek == DayOfWeek.Friday)
                {
                
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
        public override string ToString() 
        {
            return Uzenet;
        }        

    }

    class BejarhatoOsztaly : IEnumerable
    {
        List<SajatOsztaly> list = new List<SajatOsztaly>();

        public IEnumerator GetEnumerator()
        {
            return new BejaroOsztaly(list);
        }

        internal void Add(SajatOsztaly sajatOsztaly)
        {
            list.Add(sajatOsztaly);
        }
    }

    class BejaroOsztaly : IEnumerator
    {
        private List<SajatOsztaly> list;
        private int position = -1;

        public BejaroOsztaly(List<SajatOsztaly> list)
        {
            this.list = list;
        }

        public object Current
        {
            get
            {
                return list[position];
            }
        }

        public bool MoveNext()
        {
            return ++position < list.Count;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
