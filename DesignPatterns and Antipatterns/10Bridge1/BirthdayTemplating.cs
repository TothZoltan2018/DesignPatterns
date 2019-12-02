using System;

namespace _10Bridge1
{
    public class BirthdayTemplating : AbstractTemplating
    {
        protected override string GetMessage(Person person)
        {
            return $"Kedves {person.Name}! A ceg neveben szeretnenk boldog szuletesnapot kivanni!";
        }

        protected override string GetSubject(Person person)
        {
            return "Szuletesnapi udvozlet";
        }
    }
}