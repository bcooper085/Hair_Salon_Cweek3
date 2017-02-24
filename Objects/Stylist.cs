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
