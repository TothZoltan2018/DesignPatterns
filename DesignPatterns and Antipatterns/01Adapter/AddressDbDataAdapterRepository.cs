using System.Data;
using System;
using System.Collections.Generic;
using _01Adapter.Resource;

namespace _01Adapter
{
    /// <summary>
    /// Repository ----()-----> Adatok (adatbazis)
    /// A fenti eros csatolas helyett indirekcioval:
    /// Repository ----> IDbDataAdapter  -----Adatok (adatbazis)
    /// 
    /// </summary>
    public class AddressDbDataAdapterRepository : IAddressRepository
    {
        private IDbDataAdapter adapter; //IDbDataAdapter: A .NET resze

        public AddressDbDataAdapterRepository(IDbDataAdapter adapter)
        {
            this.adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public IList<Address> GetAddresses()
        {
            var dataSet = new DataSet();
            adapter.Fill(dataSet);

            var list = new List<Address>();

            foreach (DataRow row in dataSet.Tables[0].Rows) //Tobb tabla van a dataSet-ben, az elso kerjuk el
            {
                list.Add(new Address { Email = row.Field<string>(GlobalStrings.TableColumnEmailAddress) }); //A tablaban levo mezo neve, amibol ki tudjuk olvasni az adatokat: Address
            }

            return list;
        }
    }
}