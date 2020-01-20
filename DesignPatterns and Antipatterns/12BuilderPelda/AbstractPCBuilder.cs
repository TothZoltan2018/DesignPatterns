namespace _12BuilderPelda
{
    /// <summary>
    /// Ez az osszeszereles alacsony szintu lepeseinek az osztalya
    /// </summary>
    public abstract class AbstractPCBuilder
    {
        protected Computer computer;

        public void CreatePC()
        {
            computer = new Computer();
        }

        public abstract void InstallApplications();

        public abstract void InstallOS();

        public abstract void BuildHardware();

        public Computer GetPC()
        {
            return computer;
        }

    }
}