using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace BarksApp
{
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }
        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        [Fact]
        public void GetAll_IfEmptyOnLoad_Empty()
        {
            int result = Stylist.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_IfNameIsEqual_Equal()
        {
            Stylist one = new Stylist("Fran", 1);
            Stylist two = new Stylist("Fran", 1);

            Assert.Equal(one, two);
        }

        [Fact]
        public void Save_SaveStylistToDatabase_Save()
        {
            Stylist newStylist = new Stylist("Fran", 1);
            newStylist.Save();

            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testResult = new List<Stylist>{newStylist};

            Assert.Equal(testResult, result);
        }
    }
}
