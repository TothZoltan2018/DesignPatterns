using _01Adapter;
using _01Adapter.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02Strategy
{
    class AddressStrategyTestRepo : IAddressRepository
    {
        public IList<Address> GetAddresses()
        {
            return new List<Address>
            {
                new Address { Email = GlobalStrings.TestEmailAddress1, EmailCount = 2, VIP = true },
                new Address { Email = GlobalStrings.TestEmailAddress2, EmailCount = 5 },
                new Address { Email = GlobalStrings.TestEmailAddress3, EmailCount = 7 },
                new Address { Email = GlobalStrings.TestEmailAddress4, EmailCount = 1, VIP = true },
                new Address { Email = GlobalStrings.TestEmailAddress5, EmailCount = 2 },
                new Address { Email = GlobalStrings.TestEmailAddress6, EmailCount = 4 },
                new Address { Email = GlobalStrings.TestEmailAddress7, EmailCount = 11 },
                new Address { Email = GlobalStrings.TestEmailAddress8, EmailCount = 7 },
                new Address { Email = GlobalStrings.TestEmailAddress9, EmailCount = 9 },
                new Address { Email = GlobalStrings.TestEmailAddress10, EmailCount = 1 }
            };
        }
    }
}
