using _01Adapter.Resource;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Adapter.Tests
{
    public class MockDataTableFactory
    { 
        public static DataTable GetCreateDataTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(GlobalStrings.TableColumnEmailAddress, typeof(string));

            var row = dataTable.NewRow();
            row[GlobalStrings.TableColumnEmailAddress] = GlobalStrings.TestEmailAddress;
            dataTable.Rows.Add(row);
            return dataTable;
        }

        public static void CheckDataTable(DataTable table)
        {
            table.Rows.Should().HaveCount(1, "A tablaban kellett volna lennie egy sornak");
            table.Columns[GlobalStrings.TableColumnEmailAddress].Should().NotBeNull();
            table.Rows[0][GlobalStrings.TableColumnEmailAddress].Should().Be(GlobalStrings.TestEmailAddress);
        }

    }
}
