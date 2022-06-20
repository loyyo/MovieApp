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

        public Review(int id_movie, int id_user, int rate)
        {
            Id_movie = id_movie;
            Id_user = id_user;
            Rate = rate;
            Comment = string.Empty;
        }

        public Review(Review review)
        {
            Id = review.Id;
            Id_movie=review.Id_movie;
            Id_user=review.Id_user;
            Rate = review.Rate;
            Comment = review.Comment;
        }

        public override string ToString()
        {
            return Rate.ToString();
        }
    }
}
