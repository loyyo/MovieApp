using MySqlConnector;
using ProjektProgramowanie.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Repositories
{
    internal class RepositoryMovie
    {
        private const string ALL_MOVIES = "SELECT * FROM movie";

        public static List<Movie> GetAllMovies()
        {
            List<Movie> movie = new List<Movie>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_MOVIES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    movie.Add(new Movie(reader));
                connection.Close();

            }
            return movie;
        }
    }
}
