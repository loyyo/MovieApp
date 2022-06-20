using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DAL.Entities
{
    internal class Writer
    {
        public int Id;
        public string writer;

        public Writer(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            writer = reader["Writer"].ToString();
        }

        public override string ToString()
        {
            return writer;
        }
    }
}
