using MySqlConnector;
using MovieApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DAL.Repositories
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

        public static bool DeleteAddedMovieFromDB(int addedMovieID)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string DELETE_ADDEDMOVIE = $"DELETE FROM addedmovies WHERE Id={addedMovieID}";

                MySqlCommand command = new MySqlCommand(DELETE_ADDEDMOVIE, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;
                connection.Close();
            }
            return state;
        }

        public static bool EditAddedMovieInDB(AddedMovies addedMovie, int addedMovieID)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_ADDEDMOVIE = $"UPDATE addedmovies SET Id_user={addedMovie.Id_user}, Id_movie={addedMovie.Id_movie}, " +
                    $"Date='{addedMovie.Date}', List={addedMovie.List} WHERE Id={addedMovieID}";

                MySqlCommand command = new MySqlCommand(EDIT_ADDEDMOVIE, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;

                connection.Close();
            }
            return state;
        }
    }
}
