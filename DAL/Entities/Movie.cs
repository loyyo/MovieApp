using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DAL.Entities
{
    internal class Movie
    {
        public int Id;
        public string Title;
        public float Rating;
        public int Id_writer;
        public int Id_director;
        public int Id_genre;
        public string Description;
        public string Year;
        public int Length;

        public Movie(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Title = reader["Title"].ToString();
            Rating = float.Parse(reader["Rating"].ToString());
            Id_writer = int.Parse(reader["Id_writer"].ToString());
            Id_director = int.Parse(reader["Id_director"].ToString());
            Id_genre = int.Parse(reader["Id_genre"].ToString());
            Description = reader["Description"].ToString();
            Year = reader["Year"].ToString();
            Length = int.Parse(reader["Length"].ToString());
        }
    }
}
