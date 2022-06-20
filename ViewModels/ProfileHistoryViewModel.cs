using MovieApp.Models;
using MovieApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.ViewModels
{
    internal class ProfileHistoryViewModel : BaseViewModel
    {
        private HistoryItem hItem;

        public string MovieName
        {
            get { return hItem.MovieName; }
            set { hItem.MovieName = value; OnPropertyChanged(nameof(MovieName)); }
        }

        public string Description
        {
            get { return hItem.Description; }
            set { hItem.Description = value; OnPropertyChanged(nameof(Description)); }
        }

        public int Score
        {
            get { return hItem.Score; }
            set { hItem.Score = value; OnPropertyChanged(nameof(Score)); }
        }

        public string Year
        {
            get { return hItem.Year; }
            set { hItem.Year = value; OnPropertyChanged(nameof(Year)); }
        }

        public int Length
        {
            get { return hItem.Length; }
            set { hItem.Length = value; OnPropertyChanged(nameof(Length)); }
        }

        public string Genre
        {
            get { return hItem.Genre; }
            set { hItem.Genre = value; OnPropertyChanged(nameof(Genre)); }
        }
        public string List
        {
            get { return hItem.List; }
            set { hItem.List = value; OnPropertyChanged(nameof(List)); }
        }

        public string Date
        {
            get { return hItem.Date; }
            set { hItem.Date = value; OnPropertyChanged(nameof(Date)); }
        }

        public ProfileHistoryViewModel(string movieName, string description, int score, string year, int length, string genre, string list, string date)
        {
            hItem = new HistoryItem(movieName, description, score, year, length, genre, list, date);
        }
    }
}
