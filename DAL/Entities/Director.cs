using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Entities
{
    internal class Director
    {
        public int Id;
        public string director;

        public Director(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            director = reader["Director"].ToString();
        }

        public override string ToString()
        {
            return director;
        }
    }
}
