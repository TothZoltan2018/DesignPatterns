using System;
using System.Collections.Generic;

namespace _12BuilderPelda
{
    public class PCBuilderForLinux : AbstractPCBuilder
    {
        public override void InstallApplications()
        {
            computer.Applications = new List<string> { "Firefox", "Thunderbird", "VLC", "Visual Studio Code" };
        }

        public override void InstallOS()
        {
            computer.OS = OS.Ubuntu16_10;
        }

        public override void BuildHardware()
        {
            computer.Processor = Processor.amd64;
            computer.HDD = 240;
            computer.HasDVD = false;
            computer.HasSoundCard = false;
            computer.HasUSB = true;
        }
    }
}