using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DAL.Entities
{
    internal class Genre
    {
        public int Id;
        public string genre;

        public Genre(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            genre = reader["Genre"].ToString();
        }

        public override string ToString()
        {
            return genre;
        }
    }
}
