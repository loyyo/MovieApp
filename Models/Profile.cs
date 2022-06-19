using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Models
{
    internal class Profile
    {
        public string Name { get; }
        public string Surname { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; set; }
        public string Birthday { get; }
        public string Country { get; }
        public string Description { get; }
        public List<HistoryItem> AddedMovies { get; }
        public int ID { get; }

        public Profile(string name, string surname, string username, string email, string password, string birthday,
            string country, string description, List<HistoryItem> addedMovies, int id)
        {
            Name = name;
            Surname = surname;
            Username = username;
            Email = email;
            Password = password;
            Birthday = birthday;
            Country = country;
            Description = description;
            AddedMovies = addedMovies;
            ID = id;
        }
    }
}
