using MovieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    internal class AddedMoviesModel
    {
        public void FillMovies(ObservableCollection<ProfileHistoryViewModel> AddedMoviesList, List<HistoryItem> AddedMovies)
        {
            foreach (HistoryItem movie in AddedMovies.OrderByDescending(m => m.Score))
            {
                AddedMoviesList.Add(new ProfileHistoryViewModel(movie.MovieName, movie.Description, movie.Score, movie.Year, movie.Length, movie.Genre, movie.List, movie.Date));
            }
        }

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
        public void SearchAddedMovies(ObservableCollection<ProfileHistoryViewModel> AddedMoviesList,
            List<HistoryItem> AddedMovies,string SelectedScore, string SearchGenre, string SearchYear, string SearchMovieName)
        {
            List<HistoryItem> SearchedMovies = new List<HistoryItem>();
            SearchedMovies = AddedMovies;

            int SearchedScored = 0;

            if (SelectedScore != "Select")
            {
                switch (SelectedScore)
                {
                    case "(1) Appalling":
                        SearchedScored = 1;
                        break;
                    case "(2) Horrible":
                        SearchedScored = 2;
                        break;
                    case "(3) Very Bad":
                        SearchedScored = 3;
                        break;
                    case "(4) Bad":
                        SearchedScored = 4;
                        break;
                    case "(5) Average":
                        SearchedScored = 5;
                        break;
                    case "(6) Fine":
                        SearchedScored = 6;
                        break;
                    case "(7) Good":
                        SearchedScored = 7;
                        break;
                    case "(8) Very Good":
                        SearchedScored = 8;
                        break;
                    case "(9) Great":
                        SearchedScored = 9;
                        break;
                    case "(10) Masterpiece":
                        SearchedScored = 10;
                        break;
                }
                SearchedMovies = SearchedMovies.Where(m => m.Score >= SearchedScored).ToList();
            }

            if (SearchGenre != null & SearchGenre != string.Empty) SearchedMovies = SearchedMovies.Where(m => m.Genre.ToLower().Contains(SearchGenre.ToLower())).ToList();
            if (SearchYear != null & SearchYear != string.Empty) SearchedMovies = SearchedMovies.Where(m => m.Year.Contains(SearchYear)).ToList();
            if (SearchMovieName != null & SearchMovieName != string.Empty) SearchedMovies = SearchedMovies.Where(m => m.MovieName.ToLower().Contains(SearchMovieName.ToLower())).ToList();

            AddedMoviesList.Clear();
            foreach (HistoryItem movie in SearchedMovies.OrderByDescending(m => m.Score))
            {
                AddedMoviesList.Add(new ProfileHistoryViewModel
                    (movie.MovieName, movie.Description, movie.Score, movie.Year, movie.Length, movie.Genre, movie.List, movie.Date));
            }

        }
    }
}
