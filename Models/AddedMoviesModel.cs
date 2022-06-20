using ProjektProgramowanie.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Models
{
    internal class AddedMoviesModel
    {
        public void ShowMoviesFromList(ObservableCollection<ProfileHistoryViewModel> AddedMoviesList, List<HistoryItem> AddedMovies, string list)
        {
            AddedMoviesList.Clear();
            foreach (HistoryItem movie in AddedMovies.OrderByDescending(m => m.Score))
            {
                if (movie.List == list)
                    AddedMoviesList.Add(new ProfileHistoryViewModel
                        (movie.MovieName, movie.Description, movie.Score, movie.Year, movie.Length, movie.Genre, movie.List, movie.Date));
            }
        }
    }
}
