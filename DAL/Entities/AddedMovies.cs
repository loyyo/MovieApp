using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Entities
{
    internal class AddedMovies
    {
        public int Id;
        public int Id_user;
        public int Id_movie;
        public string Date;
        public int List;

        public string ToInsert()
        {
            return $"('{Id_user}', '{Id_movie}', '{Date}', '{List}')";
        }

        public AddedMovies(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Id_user = int.Parse(reader["Id_user"].ToString());
            Id_movie = int.Parse(reader["Id_movie"].ToString());
            Date = reader["Date"].ToString();
            List = bool.Parse(reader["List"].ToString()) ? 1 : 0;
        }

        public AddedMovies(int id_user, int id_movie, string date, int list)
        {
            Id_user=id_user;
            Id_movie=id_movie;
            Date = date;
            List = list;
        }

        public AddedMovies(AddedMovies addedMovies)
        {
            Id = addedMovies.Id;
            Id_user = addedMovies.Id_user;
            Id_movie = addedMovies.Id_movie;
            Date = addedMovies.Date;
            List = addedMovies.List;
        }
    }
}
