using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00Data
{
    public class AddressContext : DbContext
    {
        public AddressContext() : base("AddressDbContext") { }
        
        public DbSet<Myaddress> Myaddresses { get; set; }

    }

    /// <summary>
    /// AZ EF megkeresi, hogy  fenti DbContext mellett van -e AddressContext
    /// </summary>
    public class MyConfig : DbConfiguration
    {
        public MyConfig()
        {
            Console.WriteLine("MyConfig");
            SetExecutionStrategy("System.Data.SqlClient", () => new MyStrategy(5, TimeSpan.FromSeconds(30)));
        }
    }

    internal class MyStrategy : SqlAzureExecutionStrategy // A Microsoft strategiaja, ezt kell egy kicsit modositanunk
    {
        public MyStrategy(int maxRetryCount, TimeSpan maxDelay) : base(maxRetryCount, maxDelay)
        {         
            Console.WriteLine("MyStrategy");
        }

        protected override TimeSpan? GetNextDelay(Exception lastException)
        {
            var retval = base.GetNextDelay(lastException);
            //Console.WriteLine($"GetNextDelay: {lastException.Message}, NextDelay: {retval.ToString()}");
            Console.WriteLine($"GetNextDelay (NextDelay: {retval.ToString()})");
            return retval;
        }
        protected override bool ShouldRetryOn(Exception exception)
        {
            var errorToRetry = new int[] { -1, 109, 233 };

            var isShouldRetry = false;
            var retval = base.ShouldRetryOn(exception);

            //var sqlexception = (SqlException)exception; //vajon igy is mukodik?
            var sqlException = exception as SqlException;
            if (sqlException != null)
            {
                foreach (SqlError e in sqlException.Errors)
                {
                    Console.WriteLine($"ShouldRetryOn (Error Number: {e.Number}, ShouldRetryOn: {retval.ToString()})");
                    if (errorToRetry.Contains(e.Number))
                    {//kapcsolati hiba van
                        isShouldRetry = true;
                        Console.WriteLine("#### isShouldRetry true-ba allitva ####");
                    }
                }
            }

            Console.WriteLine($"ShouldRetryOn: (Exception: {exception.Message}, ShouldRetryOn: {retval.ToString()}, isShouldRetry: {isShouldRetry}) ");
                        
            return isShouldRetry; 
        }
    }
}
