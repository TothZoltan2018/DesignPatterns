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
    public class NormalPCDirector : AbstractPCDirector
    {
        public NormalPCDirector(AbstractPCBuilder builder) : base(builder)
        {
        }

        public override void BuildPC()
        {
            builder.CreatePC();
            builder.BuildHardware();
            builder.InstallOS();
            builder.InstallApplications();
        }
    }
}
