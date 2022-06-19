using MySqlConnector;
using ProjektProgramowanie.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Repositories
{
    internal class RepositoryDirector
    {
        private const string ALL_DIRECTORS = "SELECT * FROM director";

        public static List<Director> GetAllDirectors()
        {
            List<Director> director = new List<Director>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_DIRECTORS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    director.Add(new Director(reader));
                connection.Close();

            }
            return director;
        }
    }
}
