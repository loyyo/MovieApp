using MySqlConnector;
using ProjektProgramowanie.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Repositories
{
    internal class RepositoryAuth
    {
        private const string ALL_AUTH = "SELECT * FROM auth";
        private const string ADD_AUTH = "INSERT INTO `auth`(`Username`,`Email`,`Password`) VALUES ";

        public static List<Auth> GetAllAuth()
        {
            List<Auth> auth = new List<Auth>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_AUTH, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    auth.Add(new Auth(reader));
                connection.Close();

            }
            return auth;
        }

        public static bool AddAuthToDB(Auth auth)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_AUTH} {auth.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                auth.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        public static bool EditAuthInDB(Auth auth, int authID)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_AUTH = $"UPDATE auth SET Username='{auth.Username}', Email='{auth.Email}', " +
                    $"Password='{auth.Password}' WHERE Id={authID}";

                MySqlCommand command = new MySqlCommand(EDIT_AUTH, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;

                connection.Close();
            }
            return state;
        }
    }
}
