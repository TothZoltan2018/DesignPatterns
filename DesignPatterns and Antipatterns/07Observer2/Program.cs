using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07Observer2
{
    /// <summary>
    /// Ugyanaz, mint az elobb, csak a .Net IObserver es IObservable hasznalataval
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var f = new FelhasznaloiFelulet();

            var n = new NaplozoModul();

            var b = new BetoltoProgram();

            //A Disposable objektumokat usingban kell hasznalni. Vagy meg kell valositani az IDisposable feluletet abban ay objektumban, ahol hasznaljuk.
            using (var s1 = b.Subscribe(f))
            {
                using (var s2 = b.Subscribe(n))
                {
                    b.Start();
                }
            }

            Console.ReadLine();
        }
    }

    internal class AllapotUzenet
    {
        private int allapot;
        public int Allapot { get { return allapot; } }

        public AllapotUzenet(int allapot)
        {
            this.allapot = allapot;
        }
    }

    internal class NaplozoModul : IObserver<AllapotUzenet> //AllapotUzenet: Data Transfer Object: Megadja, hogy a megfigyelt milyen adatokat oszt meg magarol
    {
        public void OnCompleted() //Ez egy uzenet, amit ez az IObserver feluletu osztalyunk fogad: A megfigyelt kesz 
        {
            Console.WriteLine("NaplozoModul.OnCompleted()");
        }

        public void OnError(Exception error)//Ez egy uzenet, amit ez az IObserver feluletu osztalyunk fogad: A megfigyelt hibaba futott
        {
            Console.WriteLine($"NaplozoModul.OnError(){error.Message}");
        }

        public void OnNext(AllapotUzenet allapot)//Ez egy uzenet, amit ez az IObserver feluletu osztalyunk fogad: A megfigyelt normalis allapota
            //A megfigyelt sajat magat kuldi vissza
        {
            Console.WriteLine($"NaplozoModul.OnNext(){allapot.Allapot}");
        }
    }


    internal class FelhasznaloiFelulet : IObserver<AllapotUzenet> //AllapotUzenet: Data Transfer Object: Megadja, hogy a megfigyelt milyen adatokat oszt meg magarol
    {
        public void OnCompleted() //Ez egy uzenet, amit ez az IObserver feluletu osztalyunk fogad: A megfigyelt kesz 
        {
            Console.WriteLine("FelhasznaloiFelulet.OnCompleted()");
        }

        public void OnError(Exception error)//Ez egy uzenet, amit ez az IObserver feluletu osztalyunk fogad: A megfigyelt hibaba futott
        {
            Console.WriteLine($"FelhasznaloiFelulet.OnError(){error.Message}");
        }

        public void OnNext(AllapotUzenet allapot)//Ez egy uzenet, amit ez az IObserver feluletu osztalyunk fogad: A megfigyelt normalis allapota
                                                 //A megfigyelt sajat magat kuldi vissza
        {
            Console.WriteLine($"FelhasznaloiFelulet.OnNext(){allapot.Allapot}");
        }
    }

    internal class BetoltoProgram : IObservable<AllapotUzenet> //Ot lehet megfigyelni, es AllapotUzeneteket kuld magarol
    {
        List<IObserver<AllapotUzenet>> megfigyelok = new List<IObserver<AllapotUzenet>>();
        public IDisposable Subscribe(IObserver<AllapotUzenet> megfigyelo)
        {
            //A feliratkozot regisztraljuk
            if (!megfigyelok.Contains(megfigyelo))
            {
                megfigyelok.Add(megfigyelo);
            }

            //becsomagoljuk a feliratkozasi informaciokat a leiratozashoz
            return new Feliaratkozas(megfigyelok, megfigyelo);             
        }

        internal void Start()
        {
            //csinal valami hosszabb folyamatot

            //20%
            Ertesites(20);

            //40%
            Ertesites(40);

            Hiba(new Exception("Itt valami hiba tortent"));
            //70%
            Ertesites(70);

            //100%
            Ertesites(100);

            //VEGE
            Vege();
        }

        private void Hiba(Exception exception)
        {
            foreach (var megfigyelo in megfigyelok)
            {
                megfigyelo.OnError(exception);
            }
        }

        private void Vege()
        {
            foreach (var megfigyelo in megfigyelok)
            {
                megfigyelo.OnCompleted();
            }
        }

        private void Ertesites(int allapot)
        {
            foreach (var megfigyelo in megfigyelok)
            {
                megfigyelo.OnNext(new AllapotUzenet(allapot: allapot));
            }
        }
    }

    /// <summary>
    /// Ez az osztaly becsomagolja a leiratkozashoz szukseges informaciokat.
    /// Ez egy feliratkozas peldany, amig el, addig a feliratkozas is el, 
    /// amikor megszunik, az o felelossegi kore a fliratkozas megszuntetese
    /// </summary>
    internal class Feliaratkozas : IDisposable
    {
        private List<IObserver<AllapotUzenet>> megfigyelok;
        private IObserver<AllapotUzenet> megfigyelo;

        public Feliaratkozas(List<IObserver<AllapotUzenet>> megfigyelok, IObserver<AllapotUzenet> megfigyelo)
        {
            this.megfigyelok = megfigyelok;
            this.megfigyelo = megfigyelo;
        }

        /// <summary>
        /// Ha vege a feliratkozas elettartamanak, itt kell
        /// az adminisztraciot elvegezni
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("Feliratkozas.Dispose()");
            if (megfigyelok.Contains(megfigyelo))
            {
                megfigyelok.Remove(megfigyelo);
            }
            else
            {
                throw new ObjectDisposedException("Ezt mar leszedtuk a feliratkozottak listajarol");
            }
        }
    }
}
