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
        public string Password { get; }
        public string Birthday { get; }
        public string Country { get; }
        public string Description { get; }
        public List<HistoryItem> AddedMovies { get; }
        public string PrintedMovies { get; set; }

        public Profile(string name, string surname, string username, string email, string password, string birthday,
            string country, string description, List<HistoryItem> addedMovies)
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
            PrintedMovies = string.Empty;
            foreach (HistoryItem movie in AddedMovies)
            {
                PrintedMovies += "\n{\n" + movie.ToString() + "\n}";
            }
        }

        //public Profile()
        //{
        //    Name = string.Empty;
        //    Surname = string.Empty;
        //    Username = string.Empty;
        //    Email = string.Empty;
        //    Password = string.Empty;
        //    Birthday = string.Empty;
        //    Country = string.Empty;
        //    Picture = string.Empty;
        //    AddedMovies = new List<HistoryItem>();
        //    PrintedMovies = string.Empty;
        //}

        public override string ToString()
        {
            return $"Name: {Name}\nSurname: {Surname}\nUsername: {Username}\nEmail: {Email}\n" +
                $"Password: {Password}\nBirthday: {Birthday}\nCountry: {Country}\nPicture: {Description}\n" +
                $"Added Movies[]: {PrintedMovies}";
        }
    }
}
