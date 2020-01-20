using System;
using System.Collections.Generic;

namespace _12BuilderPelda
{
    public class PCBuilder
    {
        private Computer computer;        

        public void CreatePC()
        {
           computer = new Computer();            
        }

        public void BuildPC()
        {
            computer.Processor = Processor.x64;            
            computer.HDD = 120;
            computer.HasDVD = true;
            computer.HasSoundCard = true;
            computer.HasUSB = true;
            computer.OS = OS.Windows7;
            computer.Applications = new List<string> { "MSSQL", "VisualStudio", "VLC" };
            //nem kell visszaterni, mert referencia valtzot vettunk at
            //return computer;
        }

        public Computer GetPC()
        {
            return computer;
        }

    }
}