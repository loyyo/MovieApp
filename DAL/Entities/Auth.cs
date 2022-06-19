﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Entities
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
    }
}