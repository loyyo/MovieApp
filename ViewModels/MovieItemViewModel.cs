using ProjektProgramowanie.Models;
using ProjektProgramowanie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.ViewModels
{
    internal class MovieItemViewModel : BaseViewModel
    {
        private MovieItem mItem;

        public string MovieName
        {
            get { return mItem.MovieName; }
            set { mItem.MovieName = value; OnPropertyChanged(nameof(MovieName)); }
        }

        public string Description
        {
            get { return mItem.Description; }
            set { mItem.Description = value; OnPropertyChanged(nameof(Description)); }
        }

        public int Rating
        {
            get { return mItem.Rating; }
            set { mItem.Rating = value; OnPropertyChanged(nameof(Rating)); }
        }

        public string Year
        {
            get { return mItem.Year; }
            set { mItem.Year = value; OnPropertyChanged(nameof(Year)); }
        }

        public int Length
        {
            get { return mItem.Length; }
            set { mItem.Length = value; OnPropertyChanged(nameof(Length)); }
        }

        public string Genre
        {
            get { return mItem.Genre; }
            set { mItem.Genre = value; OnPropertyChanged(nameof(Genre)); }
        }
        public string Directors
        {
            get { return mItem.Directors; }
            set { mItem.Directors = value; OnPropertyChanged(nameof(Directors)); }
        }

        public string Actors
        {
            get { return mItem.Actors; }
            set { mItem.Actors = value; OnPropertyChanged(nameof(Actors)); }
        }

        public MovieItemViewModel(string movieName, string description, int rating, string year, int length, string genre, string directors, string actors)
        {
            mItem = new MovieItem(movieName, description, rating, year, length, genre, directors, actors);
        }
    }
}

