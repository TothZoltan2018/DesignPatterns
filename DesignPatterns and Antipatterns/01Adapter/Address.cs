namespace _01Adapter
{
    public class Address
    {
        public string Email { get; set; }

        //A 02Strategy projektnek kell egy szamlalo:
        public int EmailCount { get; set; }

        //A 02Strategy projektnek kell egy VIP flag:
        public bool VIP { get; set; }
    }
}