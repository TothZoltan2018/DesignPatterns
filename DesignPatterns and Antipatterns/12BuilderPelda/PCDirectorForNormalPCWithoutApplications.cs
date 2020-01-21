namespace _12BuilderPelda
{
    public class PCDirectorForNormalPCWithoutApplications : AbstractPCDirector
    {
        public PCDirectorForNormalPCWithoutApplications(AbstractPCBuilder builder) : base(builder)
        {
        }

        public override void BuildPC()
        {            
            builder.BuildHardware();
            builder.InstallOS();
        }
    }
}