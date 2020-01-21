using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12BuilderPelda
{
    class Program
    {
        static void Main(string[] args)
        {
            var director = new NormalPCDirector(new PCBuilderForWindows());

            director.BuildPC();
            var computer = director.GetPC();

            computer.Display();

            Console.Read();
        }
 
    }
}
 