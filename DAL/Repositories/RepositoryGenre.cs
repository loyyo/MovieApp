using MySqlConnector;
using ProjektProgramowanie.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Repositories
{
    internal class RepositoryGenre
    {
        private const string ALL_GENRES = "SELECT * FROM genre";

        public static List<Genre> GetAllGenres()
        {
            List<Genre> genre = new List<Genre>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_GENRES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    genre.Add(new Genre(reader));
                connection.Close();

            }
            return genre;
        }
    }
}
