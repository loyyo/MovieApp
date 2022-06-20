using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DAL.Entities
{
    internal class Auth
    {
        public int Id;
        public string Username;
        public string Email;
        public string Password;

        public string ToInsert()
        {
            return $"('{Username}', '{Email}', '{Password}')";
        }

        public Auth(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Username = reader["Username"].ToString();
            Email = reader["Email"].ToString();
            Password = reader["Password"].ToString();
        }

        public Auth(string username, string email, string password)
        {
            Username = username.Trim();
            Email = email.Trim();
            Password = password.Trim();
        }

        public Auth(Auth auth)
        {
            Id = auth.Id;
            Username = auth.Username;
            Email = auth.Email;
            Password = auth.Password;
        }
    }
}
