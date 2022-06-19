using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Entities
{
    internal class User
    {
        public int Id;
        public int Id_auth;
        public string Name;
        public string Last_Name;
        public string Description;
        public string Birthday;
        public string Country;

        public string ToInsert()
        {
            return $"('{Id_auth}', '{Name}', '{Last_Name}', '{Description}', '{Birthday}', '{Country}')";
        }

        public User(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Id_auth = int.Parse(reader["Id_auth"].ToString());
            Name = reader["Name"].ToString();
            Last_Name = reader["Last_Name"].ToString();
            Description = reader["Description"].ToString();
            Birthday = reader["Birthday"].ToString().Substring(0, 10);
            Country = reader["Country"].ToString();
        }
    }
}
