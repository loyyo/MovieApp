using MySqlConnector;
using ProjektProgramowanie.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Repositories
{
    internal class RepositoryAddedMovies
    {
        private const string ALL_ADDEDMOVIES = "SELECT * FROM addedmovies";
        private const string ADD_ADDEDMOVIE = "INSERT INTO `addedmovies`(`Id_user`,`Id_movie`,`Date`, `List`) VALUES ";

        public static List<AddedMovies> GetAllAddedMovies()
        {
            List<AddedMovies> addedMovies = new List<AddedMovies>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_ADDEDMOVIES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    addedMovies.Add(new AddedMovies(reader));
                connection.Close();

            }
            return addedMovies;
        }

        public static bool AddAddedMovieToDB(AddedMovies addedMovie)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_ADDEDMOVIE} {addedMovie.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                addedMovie.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        public static bool DeleteAddedMovieFromDB(AddedMovies addedMovie)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_ADDEDMOVIE} {addedMovie.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                addedMovie.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }
    }
}
