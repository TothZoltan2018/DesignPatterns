using System;
using System.Collections.Generic;
using System.Linq;
using _01Adapter;

namespace _02Strategy
{
    public class SumStrategy : IStrategy //geeraltuk ezt az Interfeszt, amivel majd mas strategiakat is tudunk csinalni
    {
        public int Operation(IList<Address> list)
        {
            return list.Sum(x => x.EmailCount);
        }
    }
}