using MovieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    internal class SearchMovies
    {
        public void FillMovies(ObservableCollection<MovieItemViewModel> AllMoviesList, List<MovieItem> AllMovies)
        {
            foreach (MovieItem movie in AllMovies.OrderByDescending(m => m.Rating))
            {
                AllMoviesList.Add(new MovieItemViewModel(movie.MovieName, movie.Description, movie.Rating, movie.Year, movie.Length, movie.Genre, movie.Directors, movie.Writers, movie.ID));
            }
        }

        public void SearchAllMovies(ObservableCollection<MovieItemViewModel> AllMoviesList,
    List<MovieItem> AllMovies, string SelectedRating, string SearchGenre, string SearchYear, string SearchMovieName)
        {
            List<MovieItem> SearchedMovies = new List<MovieItem>();
            SearchedMovies = AllMovies;

            int SearchedRating = 0;

            if (SelectedRating != "Select")
            {
                switch (SelectedRating)
                {
                    case "(1) Appalling":
                        SearchedRating = 1;
                        break;
                    case "(2) Horrible":
                        SearchedRating = 2;
                        break;
                    case "(3) Very Bad":
                        SearchedRating = 3;
                        break;
                    case "(4) Bad":
                        SearchedRating = 4;
                        break;
                    case "(5) Average":
                        SearchedRating = 5;
                        break;
                    case "(6) Fine":
                        SearchedRating = 6;
                        break;
                    case "(7) Good":
                        SearchedRating = 7;
                        break;
                    case "(8) Very Good":
                        SearchedRating = 8;
                        break;
                    case "(9) Great":
                        SearchedRating = 9;
                        break;
                    case "(10) Masterpiece":
                        SearchedRating = 10;
                        break;
                }
                SearchedMovies = SearchedMovies.Where(m => m.Rating >= SearchedRating).ToList();
            }

            if (SearchGenre != null & SearchGenre != string.Empty) SearchedMovies = SearchedMovies.Where(m => m.Genre.ToLower().Contains(SearchGenre.ToLower())).ToList();
            if (SearchYear != null & SearchYear != string.Empty) SearchedMovies = SearchedMovies.Where(m => m.Year.Contains(SearchYear)).ToList();
            if (SearchMovieName != null & SearchMovieName != string.Empty) SearchedMovies = SearchedMovies.Where(m => m.MovieName.ToLower().Contains(SearchMovieName.ToLower())).ToList();

            AllMoviesList.Clear();
            foreach (MovieItem movie in SearchedMovies.OrderByDescending(m => m.Rating))
            {
                AllMoviesList.Add(new MovieItemViewModel
                    (movie.MovieName, movie.Description, movie.Rating, movie.Year, movie.Length, movie.Genre, movie.Directors, movie.Writers, movie.ID));
            }

        }
    }
}
