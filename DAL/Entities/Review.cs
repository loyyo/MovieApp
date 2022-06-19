using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Entities
{
    internal class Review
    {
        public int Id;
        public int Id_movie;
        public int Id_user;
        public int Rate;
        public string Comment;

        public string ToInsert()
        {
            return $"('{Id_movie}', '{Id_user}', '{Rate}', '{Comment}')";
        }

        public Review(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Id_movie = int.Parse(reader["Id_movie"].ToString());
            Id_user = int.Parse(reader["Id_user"].ToString());
            Rate = int.Parse(reader["Rate"].ToString());
            Comment = reader["Comment"].ToString();
        }

        public override string ToString()
        {
            return Rate.ToString();
        }
    }
}
