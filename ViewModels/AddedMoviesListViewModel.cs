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
    internal class AddedMoviesListViewModel : BaseViewModel
    {
        public AddedMoviesListViewModel(
            ProfileStore profileStore,
            Baza movies,
            MovieStore movieStore,
            NavigationService movieNavigationService,
            NavigationService profileViewNavigationService,
            NavigationService accountSettingsViewNavigationService,
            NavigationService searchMoviesListViewNavigationService)
        {
            _profile = profileStore;
            _movies = movies;
            _movieStore = movieStore;

            _movieNavigationService = movieNavigationService;
            GoToProfileView = new NavigateCommand(profileViewNavigationService);
            GoToAccountSettingsView = new NavigateCommand(accountSettingsViewNavigationService);
            GoToSearchMoviesListView = new NavigateCommand(searchMoviesListViewNavigationService);
            AddedMoviesList = new ObservableCollection<ProfileHistoryViewModel>();

            foreach (HistoryItem movie in AddedMovies.OrderByDescending(m => m.Score))
            {
                AddedMoviesList.Add(new ProfileHistoryViewModel(movie.MovieName, movie.Description, movie.Score, movie.Year, movie.Length, movie.Genre, movie.List, movie.Date));
            }
            ComboBoxScores = new ObservableCollection<string> { "Select", "(10) Masterpiece", "(9) Great", "(8) Very Good",
                "(7) Good", "(6) Fine", "(5) Average", "(4) Bad", "(3) Very Bad", "(2) Horrible", "(1) Appalling" };
            SelectedScore = "Select";

        }

        private readonly NavigationService _movieNavigationService;
        private readonly ProfileStore _profile;
        private readonly Baza _movies;
        private readonly MovieStore _movieStore;

        public ICommand GoToProfileView { get; }
        public ICommand GoToAccountSettingsView { get; }
        public ICommand GoToSearchMoviesListView { get; }

        public List<HistoryItem> AddedMovies => _profile.CurrentProfile.AddedMovies;

        private ObservableCollection<ProfileHistoryViewModel> _addedMoviesList;
        public ObservableCollection<ProfileHistoryViewModel> AddedMoviesList
        {
            get { return _addedMoviesList; }
            set { _addedMoviesList = value; OnPropertyChanged(nameof(AddedMoviesList)); }
        }

        private ObservableCollection<string> _comboBoxScores;
        public ObservableCollection<string> ComboBoxScores
        {
            get { return _comboBoxScores; }
            set { _comboBoxScores = value; OnPropertyChanged(nameof(ComboBoxScores)); }
        }

        private string _selectedScore;
        public string SelectedScore
        {
            get { return _selectedScore; }
            set { _selectedScore = value; OnPropertyChanged(nameof(SelectedScore)); }
        }

        private string _searchMovieName;
        public string SearchMovieName
        {
            get { return _searchMovieName; }
            set { _searchMovieName = value; OnPropertyChanged(nameof(SearchMovieName)); }
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

        private ProfileHistoryViewModel _selectedItem;
        public ProfileHistoryViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        private RelayCommand? _showWatchedMovies;
        public RelayCommand ShowWatchedMovies
        {
            get
            {
                if (_showWatchedMovies == null)
                    _showWatchedMovies = new RelayCommand(argument =>
                    {
                        AddedMoviesList.Clear();
                        foreach (HistoryItem movie in AddedMovies.OrderByDescending(m => m.Score))
                        {
                            if (movie.List == "Watched")
                                AddedMoviesList.Add(new ProfileHistoryViewModel
                                    (movie.MovieName, movie.Description, movie.Score, movie.Year, movie.Length, movie.Genre, movie.List, movie.Date));
                        }
                    }, argument => true);
                return _showWatchedMovies;
            }
        }

        private RelayCommand? _showPTWMovies;
        public RelayCommand ShowPTWMovies
        {
            get
            {
                if (_showPTWMovies == null)
                    _showPTWMovies = new RelayCommand(argument =>
                    {
                        AddedMoviesList.Clear();
                        foreach (HistoryItem movie in AddedMovies.OrderByDescending(m => m.Score))
                        {
                            if (movie.List == "PTW")
                                AddedMoviesList.Add(new ProfileHistoryViewModel
                                    (movie.MovieName, movie.Description, movie.Score, movie.Year, movie.Length, movie.Genre, movie.List, movie.Date));
                        }
                    }, argument => true);
                return _showPTWMovies;
            }
        }

        private RelayCommand? _searchAddedCommand;
        public RelayCommand SearchAddedCommand
        {
            get
            {
                if (_searchAddedCommand == null)
                    _searchAddedCommand = new RelayCommand(argument =>
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

                    }, argument => true);
                return _searchAddedCommand;
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
