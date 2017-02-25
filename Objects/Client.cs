using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace BarksApp
{
    public class Client
    {
        private string _name;
        private int _id;
        private int _stylistId;

        public Client(string name, int stylistId, int Id = 0)
        {
            _name = name;
            _stylistId = stylistId;
            _id = Id;
        }
        public string GetName()
        {
            return _name;
        }
        public int GetStylistId()
        {
            return _stylistId;
        }
        public int GetId()
        {
            return _id;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>{};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int foundId = rdr.GetInt32(0);
                string foundName = rdr.GetString(1);
                int foundStylist = rdr.GetInt32(2);
                Client foundClient = new Client(foundName, foundStylist, foundId);
                allClients.Add(foundClient);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                rdr.Close();
            }

            return allClients;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO clients(name, stylist_id) OUTPUT INSERTED.id VALUES(@ClientName, @StylistId)", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@ClientName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);

            SqlParameter stylistParameter = new SqlParameter();
            stylistParameter.ParameterName = "@StylistId";
            stylistParameter.Value = this.GetStylistId();
            cmd.Parameters.Add(stylistParameter);


            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            if(rdr != null)
            {
                rdr.Close();
            }

            if(conn != null)
            {
                conn.Close();
            }
        }
        public void UpdateName(string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName OUTPUT INSERTED.name WHERE id = @ClientId;", conn);
            SqlParameter newNameParameter = new SqlParameter();
            newNameParameter.ParameterName = "@NewName";
            newNameParameter.Value = newName;
            cmd.Parameters.Add(newNameParameter);

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@ClientId";
            idParameter.Value = this.GetId();
            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._name = rdr.GetString(0);
            }

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Client Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@ClientId";
            idParameter.Value = id;

            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = null;
            int foundStylist = 0;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundStylist = rdr.GetInt32(2);
            }

            Client foundClient = new Client(foundName, foundStylist, foundId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return foundClient;
        }

        public static void DeleteClient(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@ClientId";
            idParameter.Value = id;
            cmd.Parameters.Add(idParameter);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


//Override
        public override bool Equals(System.Object otherClient)
        {
            if(!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = (this.GetId() == newClient.GetId());
                bool nameEquality = (this.GetName() == newClient.GetName());
                bool stylistEquality = (this.GetStylistId() == newClient.GetStylistId());
                return(idEquality && nameEquality && stylistEquality);
            }
        }
    }
}
