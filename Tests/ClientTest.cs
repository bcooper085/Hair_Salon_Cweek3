using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace BarksApp
{
    public class ClientTest : IDisposable
    {
        public ClientTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }
        public void Dispose()
        {
            Client.DeleteAll();
        }

        [Fact]
        public void GetAll_IfEmptyOnLoad_Empty()
        {
            int result = Client.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_IfNameIsEqual_Equal()
        {
            Client one = new Client("Fran", 1);
            Client two = new Client("Fran", 1);

            Assert.Equal(one, two);
        }

        [Fact]
        public void Save_SaveClientToDatabase_Save()
        {
            Client newClient = new Client("Scruff", 1);
            newClient.Save();

            List<Client> result = Client.GetAll();
            List<Client> testResult = new List<Client>{newClient};

            Assert.Equal(testResult, result);
        }

        [Fact]
        public void UpdateName_EditClientInDatabase_Edit()
        {
            Client newClient = new Client("Fran", 1);
            newClient.Save();

            newClient.UpdateName("Francisco");

            Assert.Equal("Francisco", newClient.GetName());
        }

        [Fact]
        public void Find_FindSearchedStylist_Find()
        {
            Client newClient = new Client("Fran", 1);
            newClient.Save();


            Client foundClient = Client.Find(newClient.GetId());

            Assert.Equal(newClient, foundClient);
        }

        [Fact]
        public void Delete_DeleteClientFromDatabase_Delete()
        {
            Client newClient = new Client("Fran", 1);
            newClient.Save();
            Client.DeleteClient(newClient.GetId());

            List<Client> testResult = new List<Client>{};
            List<Client> result = Client.GetAll();

            Assert.Equal(testResult, result);
        }
    }
}
