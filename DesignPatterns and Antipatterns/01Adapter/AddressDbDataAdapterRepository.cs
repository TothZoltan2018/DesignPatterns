using System.Data;
using System;
using System.Collections.Generic;

namespace _01Adapter
{
    public class AddressDbDataAdapterRepository : IAddressRepository
    {
        private IDbDataAdapter adapter; //IDbDataAdapter: A .NET resze

        public AddressDbDataAdapterRepository(IDbDataAdapter adapter)
        {
            this.adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public IList<Address> GetAddresses()
        {
            throw new NotImplementedException();
        }
    }
}