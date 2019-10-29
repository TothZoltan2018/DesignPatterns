using System.Collections.Generic;
using System.Linq;
using _01Adapter;
using System;

namespace _02Strategy
{
    public class AvgStrategy : IStrategy
    {
        public int Operation(IList<Address> list)
        {
            var avg = list.Average(x => x.EmailCount);
            return (int)Math.Round(avg);
        }
    }
}