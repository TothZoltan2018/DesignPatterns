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
            var builder1 = new PCBuilder();

            builder1.CreatePC();
            builder1.BuildPC();
            var computer = builder1.GetPC();
                        
            computer.Display();

            Console.Read();
        }
 
    }
}
 