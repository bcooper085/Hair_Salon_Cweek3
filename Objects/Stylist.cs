using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace BarksApp
{
    public class Stylist
    {
        private string _name;
        private int _id;
        private int _clientId;

        public Stylist(string name, int clientId, int Id = 0)
        {
            _name = name;
            _clientId = clientId;
            _id = Id;
        }
        public string GetName()
        {
            return _name;
        }
        public int GetClientId()
        {
            return _clientId;
        }
        public int GetId()
        {
            return _id;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist>();
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int foundId = rdr.GetInt32(0);
                string foundName = rdr.GetString(1);
                int foundClient = rdr.GetInt32(2);
                Stylist foundStylist = new Stylist(foundName, foundClient, foundId);
                allStylists.Add(foundStylist);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                rdr.Close();
            }

            return allStylists;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO stylists(name, client_Id) OUTPUT INSERTED.id VALUES(@StylistName, @ClientId)", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@StylistName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);

            SqlParameter clientParameter = new SqlParameter();
            clientParameter.ParameterName = "@ClientId";
            clientParameter.Value = this.GetClientId();
            cmd.Parameters.Add(clientParameter);

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

            SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @StylistId;", conn);
            SqlParameter newNameParameter = new SqlParameter();
            newNameParameter.ParameterName = "@NewName";
            newNameParameter.Value = newName;
            cmd.Parameters.Add(newNameParameter);

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@StylistId";
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

        public static Stylist Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@StylistId";
            idParameter.Value = id.ToString();

            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = null;
            int foundClient = 0;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundClient = rdr.GetInt32(2);
            }

            Stylist foundStylist = new Stylist(foundName, foundClient, foundId);

            return foundStylist;
        }

        public static void DeleteStylist(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId;", conn);

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@StylistId";
            idParameter.Value = id;
            cmd.Parameters.Add(idParameter);
            cmd.ExecuteNonQuery();
            conn.Close();
        }





//Override
        public override bool Equals(System.Object otherStylist)
        {
            if(!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool idEquality = (this.GetId() == newStylist.GetId());
                bool nameEquality = (this.GetName() == newStylist.GetName());
                bool clientEquality = (this.GetClientId() == newStylist.GetClientId());
                return(idEquality && nameEquality && clientEquality);
            }
        }
    }
}
