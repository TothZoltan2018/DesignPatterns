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
            var director1 = new PCDirectorForNormalPCWithApplications(new PCBuilderForWindows());
            director1.BuildPC();
            var computer1 = director1.GetPC();
            computer1.Display();

            var director2 = new PCDirectorForNormalPCWithoutApplications(new PCBuilderForLinux());
            director2.BuildPC();
            var computer2 = director2.GetPC();
            computer2.Display();

            Console.Read();
        }
 
    }
}
 