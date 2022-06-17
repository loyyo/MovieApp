using ProjektProgramowanie.Commands;
using ProjektProgramowanie.Models;
using ProjektProgramowanie.Services;
using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektProgramowanie.ViewModels
{
    internal class SearchMoviesListViewModel : BaseViewModel
    {
        public SearchMoviesListViewModel(
            Movies movies,
            MovieStore movieStore,
            NavigationService movieNavigationService,
            NavigationService profileViewNavigationService,
            NavigationService accountSettingsViewNavigationService,
            NavigationService addedMoviesListViewNavigationService)
        {
            _movies = movies;
            _movieStore = movieStore;
            _movieNavigationService = movieNavigationService;
            GoToProfileView = new NavigateCommand(profileViewNavigationService);
            GoToAccountSettingsView = new NavigateCommand(accountSettingsViewNavigationService);
            GoToAddedMoviesListView = new NavigateCommand(addedMoviesListViewNavigationService);
            AllMoviesList = new ObservableCollection<MovieItemViewModel>();
            foreach (MovieItem movie in _movies._moviesList.OrderByDescending(m => m.Rating))
            {
                AllMoviesList.Add(new MovieItemViewModel(movie.MovieName, movie.Description, movie.Rating, movie.Year, movie.Length, movie.Genre, movie.Directors, movie.Actors));
            }
            ComboBoxRatings = new ObservableCollection<string> { "Select", "(10) Masterpiece", "(9) Great", "(8) Very Good",
                "(7) Good", "(6) Fine", "(5) Average", "(4) Bad", "(3) Very Bad", "(2) Horrible", "(1) Appalling" };
            SelectedRating = "Select";
        }

        private readonly NavigationService _movieNavigationService;
        private readonly Movies _movies;
        private readonly MovieStore _movieStore;

        public ICommand GoToProfileView { get; }
        public ICommand GoToAccountSettingsView { get; }
        public ICommand GoToAddedMoviesListView { get; }

        private ObservableCollection<MovieItemViewModel> _allMoviesList;
        public ObservableCollection<MovieItemViewModel> AllMoviesList
        {
            get { return _allMoviesList; }
            set { _allMoviesList = value; OnPropertyChanged(nameof(AllMoviesList)); }
        }

        private ObservableCollection<string> _comboBoxRatings;
        public ObservableCollection<string> ComboBoxRatings
        {
            get { return _comboBoxRatings; }
            set { _comboBoxRatings = value; OnPropertyChanged(nameof(ComboBoxRatings)); }
        }

        private string _selectedRating;
        public string SelectedRating
        {
            get { return _selectedRating; }
            set { _selectedRating = value; OnPropertyChanged(nameof(SelectedRating)); }
        }

        private string _serachMovieName;
        public string SearchMovieName
        {
            get { return _serachMovieName; }
            set { _serachMovieName = value; OnPropertyChanged(nameof(SearchMovieName)); }
        }

        private string _searchYear;
        public string SearchYear
        {
            get { return _searchYear; }
            set { _searchYear = value; OnPropertyChanged(nameof(SearchYear)); }
        }

        private string _searchGenre;
        public string SearchGenre
        {
            get { return _searchGenre; }
            set { _searchGenre = value; OnPropertyChanged(nameof(SearchGenre)); }
        }

        private MovieItemViewModel _selectedItem;
        public MovieItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        private RelayCommand? _searchMoviesCommand;
        public RelayCommand SearchMoviesCommand
        {
            get
            {
                if (_searchMoviesCommand == null)
                    _searchMoviesCommand = new RelayCommand(argument =>
                    {
                        List<MovieItem> SearchedMovies = new List<MovieItem>();
                        SearchedMovies = _movies._moviesList;

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
                                (movie.MovieName, movie.Description, movie.Rating, movie.Year, movie.Length, movie.Genre, movie.Directors, movie.Actors));
                        }

                    }, argument => true);
                return _searchMoviesCommand;
            }
        }
        private RelayCommand? _doubleClickCommand;
        public RelayCommand DoubleClickCommand
        {
            get
            {
                if (_doubleClickCommand == null)
                    _doubleClickCommand = new RelayCommand(argument =>
                    {
                        _movieStore.Movie = _movies._moviesList.Where(x => x.MovieName == SelectedItem.MovieName).First();
                        _movieNavigationService.Navigate();

                    }, argument => true);
                return _doubleClickCommand;
            }
        }
    }
}
