namespace _09Bridge
{
    public class EmailMessage
    {
        public EmailAddress From { get; set; }
        public EmailAddress To { get; set; }
        public string Suject { get; set; }
        public string Message { get; set; }
    }
}