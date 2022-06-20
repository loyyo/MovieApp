using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL
{
    internal class DBConnection
    {
        private MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();

        private static DBConnection? _instance = null;

        public static DBConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DBConnection();
                return _instance;
            }
        }

        public MySqlConnection Connection => new MySqlConnection(stringBuilder.ToString());

        private DBConnection()
        {
            stringBuilder.UserID = Settings1.Default.userID;
            stringBuilder.Server = Settings1.Default.server;
            stringBuilder.Database = Settings1.Default.database;
            stringBuilder.Port = Settings1.Default.port;
            stringBuilder.Password = Settings1.Default.passwd;
        }
    }
}
