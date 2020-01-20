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
            var builder = new PCBuilderForWindows();

            builder.CreatePC();
            builder.BuildPC();
            var computer = builder.GetPC();
                        
            computer.Display();

            Console.Read();
        }
 
    }
}
 