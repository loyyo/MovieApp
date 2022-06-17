using ProjektProgramowanie.Commands;
using ProjektProgramowanie.Models;
using ProjektProgramowanie.Services;
using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektProgramowanie.ViewModels
{
    internal class MoviePageViewModel : BaseViewModel
    {
        public MoviePageViewModel(
            ProfileStore profileStore,
            MovieStore movieStore,
            NavigationService profileViewNavigationService,
            NavigationService accountSettingsViewNavigationService,
            NavigationService searchMoviesListViewNavigationService,
            NavigationService addedMoviesListViewNavigationService)
        {
            _profile = profileStore;
            _movie = movieStore;
            GoToProfileView = new NavigateCommand(profileViewNavigationService);
            GoToAccountSettingsView = new NavigateCommand(accountSettingsViewNavigationService);
            GoToSearchMoviesListView = new NavigateCommand(searchMoviesListViewNavigationService);
            GoToAddedMoviesListView = new NavigateCommand(addedMoviesListViewNavigationService);

            _profileMovie = _profile.CurrentProfile.AddedMovies.Where(x => x.MovieName == _movie.Movie.MovieName).First();

            switch (_profileMovie.Score)
            {
                case 1:
                    SelectedScore = "(1) Appalling";
                    break;
                case 2:
                    SelectedScore = "(2) Horrible";
                    break;
                case 3:
                    SelectedScore = "(3) Very Bad";
                    break;
                case 4:
                    SelectedScore = "(4) Bad";
                    break;
                case 5:
                    SelectedScore = "(5) Average";
                    break;
                case 6:
                    SelectedScore = "(6) Fine";
                    break;
                case 7:
                    SelectedScore = "(7) Good";
                    break;
                case 8:
                    SelectedScore = "(8) Very Good";
                    break;
                case 9:
                    SelectedScore = "(9) Great";
                    break;
                case 10:
                    SelectedScore = "(10) Masterpiece";
                    break;
            }

            ComboBoxScores = new ObservableCollection<string> { "Select", "(10) Masterpiece", "(9) Great", "(8) Very Good",
                "(7) Good", "(6) Fine", "(5) Average", "(4) Bad", "(3) Very Bad", "(2) Horrible", "(1) Appalling" };
            SelectedScore = "Select";
        }

        private readonly ProfileStore _profile;
        private readonly MovieStore _movie;
        private readonly HistoryItem _profileMovie;

        public ICommand GoToProfileView { get; }
        public ICommand GoToAccountSettingsView { get; }
        public ICommand GoToSearchMoviesListView { get; }
        public ICommand GoToAddedMoviesListView { get; }

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

        private string _movieName;
        public string MovieName
        {
            get { return _movieName; }
            set { _movieName = value; OnPropertyChanged(nameof(MovieName)); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        private float _rating;
        public float Rating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(nameof(Rating)); }
        }

        private string _year;
        public string Year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged(nameof(Year)); }
        }

        private int _length;
        public int Length
        {
            get { return _length; }
            set { _length = value; OnPropertyChanged(nameof(Length)); }
        }

        private string _genres;
        public string Genres
        {
            get { return _genres; }
            set { _genres = value; OnPropertyChanged(nameof(Genres)); }
        }

        private string _directors;
        public string Directors
        {
            get { return _directors; }
            set { _directors = value; OnPropertyChanged(nameof(Directors)); }
        }

        private string _actors;
        public string Actors
        {
            get { return _actors; }
            set { _actors = value; OnPropertyChanged(nameof(Actors)); }
        }

        private RelayCommand? _addToPTWCommand;
        public RelayCommand AddToPTWCommand
        {
            get
            {
                if (_addToPTWCommand == null)
                    _addToPTWCommand = new RelayCommand(argument =>
                    {
                        //funkcjonalność HERE
                    }, argument => true);
                return _addToPTWCommand;
            }
        }

        private RelayCommand? _addToWatchedCommand;
        public RelayCommand AddToWatchedCommand
        {
            get
            {
                if (_addToWatchedCommand == null)
                    _addToWatchedCommand = new RelayCommand(argument =>
                    {
                        //funkcjonalność HERE
                    }, argument => true);
                return _addToWatchedCommand;
            }
        }
    }
}
