using MySqlConnector;
using ProjektProgramowanie.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Repositories
{
    internal class RepositoryUser
    {
        private const string ALL_USERS = "SELECT * FROM user";
        private const string ADD_USER = "INSERT INTO `user`(`Id_auth`, `Name`, `Last_name`, `Description`, `Birthday`, `Country`) VALUES ";

        public static List<User> GetAllUsers()
        {
            List<User> user = new List<User>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_USERS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    user.Add(new User(reader));
                connection.Close();

            }
            return user;
        }

        public static bool AddUserToDB(User user)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_USER} {user.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                user.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }
    }
}
