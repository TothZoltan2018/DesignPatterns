using _01Adapter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _02Strategy
{
    public class DataService
    {
        protected IAddressRepository repository; //protected, hogy lassa a leszarmaztatott (DataService2) osztaly is
        //private SumStrategy strategy;
        private IStrategy strategy;

        public DataService(IAddressRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //public DataService(IAddressRepository repository, SumStrategy strategy) : this(repository)
        public DataService(IAddressRepository repository, IStrategy strategy) : this(repository)
        {
            this.strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));            
        }

        public int ReportWithStrategy()
        {
            var list = repository.GetAddresses();
            return strategy.Operation(list);
        }

        public int ReportWithDelegate(Func<IList<Address>, int> strategy) //A strategy egy Addresslista feluletet varo, intet visszaado delegate
        {
            if (strategy == null) throw new ArgumentNullException(nameof(strategy));
                       
            return strategy(repository.GetAddresses()); //Vegrehajtja a delegate-nek megfelelo, kintrol megkapott fgv-t.
        }

        #region nem jo megoldasok
        public int GetSumEmailCount()
        {
            return repository.GetAddresses().Sum(x => x.EmailCount); 
        }

        public int GetAvgEmailCount()
        {
            var avg = repository.GetAddresses().Average(x => x.EmailCount);
            return (int)Math.Round(avg);
        }

        public virtual int Report(ReportType rep)
        {
            switch (rep)
            {
                case ReportType.Sum:
                    return GetSumEmailCount();                    
                case ReportType.Average:
                    return GetAvgEmailCount();
                default:
                    throw new ArgumentOutOfRangeException($"nameof(rep): {rep}");                    
            }

        }


    }

    public enum ReportType { Sum, Average }

    public class DataService2 : DataService
    {
        //private IAddressRepository repository; //az ososztalybol latszik, mert ott protected

        public DataService2(IAddressRepository repository)
            : base(repository) //mivel nincs alapetelmezett konstruktora, hiszem defialtam egy parametereset
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //public new int Report(ReportType rep)
        //{            
        //}

        public override int Report(ReportType rep)
        {
            return repository.GetAddresses().Where(x => x.VIP == true).Sum(x => x.EmailCount);            
        }


    }
#endregion
}   