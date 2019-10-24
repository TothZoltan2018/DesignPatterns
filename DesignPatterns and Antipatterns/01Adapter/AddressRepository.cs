using System;
using System.Collections.Generic;

namespace _01Adapter
{
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository()
        {
        }

        public IList<Address> GetAddresses()
        {
            return new List<Address> { new Address { Email = "TZ@gmail.com" } }; //Todo: ez az osszedrotozas nem a vegleges megodas lesz
        }
    }
}