using MySqlConnector;
using MovieApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DAL.Repositories
{
    internal class RepositoryWriter
    {
        private const string ALL_WRITERS = "SELECT * FROM writer";

        public static List<Writer> GetAllWriters()
        {
            List<Writer> writer = new List<Writer>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_WRITERS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    writer.Add(new Writer(reader));
                connection.Close();

            }
            return writer;
        }
    }
}
