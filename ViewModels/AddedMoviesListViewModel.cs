using MovieApp.Commands;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Stores;
using MovieApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieApp.ViewModels
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

            _addedMoviesModel.FillMovies(AddedMoviesList, AddedMovies);

            ComboBoxScores = new ObservableCollection<string> { "Select", "(10) Masterpiece", "(9) Great", "(8) Very Good",
                "(7) Good", "(6) Fine", "(5) Average", "(4) Bad", "(3) Very Bad", "(2) Horrible", "(1) Appalling" };
            SelectedScore = "Select";

        }

        private readonly AddedMoviesModel _addedMoviesModel = new AddedMoviesModel();
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
                        _addedMoviesModel.ShowMoviesFromList(AddedMoviesList, AddedMovies, "Watched");
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
                        _addedMoviesModel.ShowMoviesFromList(AddedMoviesList, AddedMovies, "PTW");
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
                        _addedMoviesModel.SearchAddedMovies(AddedMoviesList, AddedMovies, SelectedScore, SearchGenre, SearchYear, SearchMovieName);
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
