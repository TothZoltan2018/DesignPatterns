using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12BuilderPelda
{
    /// <summary>
    /// A letrehozas magasabb szintu vezerloje, a builder lepeseit iranyitja
    /// </summary>
    class NormalPCDirector
    {
        private AbstractPCBuilder builder;

        public NormalPCDirector(AbstractPCBuilder builder)
        {
            this.builder = builder;
        }

        public void BuildPC()
        {
            builder.CreatePC();
            builder.BuildHardware();
            builder.InstallOS();
            builder.InstallApplications();
        }

        public Computer GetPC()
        {
            return builder.GetPC();
        }
    }
}
