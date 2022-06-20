using MovieApp.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
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
        public int ID { get; set; }

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

        public void FillProfileHistory(Profile profil, ObservableCollection<ProfileHistoryViewModel> HistoryList)
        {
            profil.AddedMovies.Sort((x, y) => DateTime.Compare(DateTime.Parse(y.Date), DateTime.Parse(x.Date)));
            foreach (HistoryItem movie in profil.AddedMovies)
            {
                HistoryList.Add(new ProfileHistoryViewModel(movie.MovieName, movie.Description, movie.Score, movie.Year, movie.Length, movie.Genre, movie.List, movie.Date));
            }
        }
    }
}
