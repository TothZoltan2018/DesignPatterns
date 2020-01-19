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
            var computer = new Computer();

            computer.Processor = Processor.x64;
            computer.OS = OS.Windows7;
            computer.HDD = 120;
            computer.HasDVD = true;
            computer.HasSoundCard = true;
            computer.HasUSB = true;
            computer.Applications = new List<string> { "MSSQL", "VisualStudio", "VLC"};
            computer.Display();

            Console.Read();
        }
    }
}
 