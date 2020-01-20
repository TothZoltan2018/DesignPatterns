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
            var builder1 = new PCBuilderForWindows();

            builder1.CreatePC();
            builder1.BuildPC();
            var computer1 = builder1.GetPC();
                        
            computer1.Display();

            Console.WriteLine("-------------------------------------");

            var builder2 = new PCBuilderForLinux();

            builder2.CreatePC();
            builder2.BuildPC();
            var computer2 = builder2.GetPC();

            computer2.Display();

            Console.Read();
        }
 
    }
}
 