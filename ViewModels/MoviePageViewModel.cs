using ProjektProgramowanie.Commands;
using ProjektProgramowanie.DAL.Entities;
using ProjektProgramowanie.DAL.Repositories;
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
using System.Windows;
using System.Windows.Input;

namespace ProjektProgramowanie.ViewModels
{
    internal class MoviePageViewModel : BaseViewModel
    {
        public MoviePageViewModel(
            Baza users,
            ProfileStore profileStore,
            MovieStore movieStore,
            NavigationService profileViewNavigationService,
            NavigationService accountSettingsViewNavigationService,
            NavigationService searchMoviesListViewNavigationService,
            NavigationService addedMoviesListViewNavigationService)
        {
            _users = users;
            _profile = profileStore;
            _movie = movieStore;
            GoToProfileView = new NavigateCommand(profileViewNavigationService);
            GoToAccountSettingsView = new NavigateCommand(accountSettingsViewNavigationService);
            GoToSearchMoviesListView = new NavigateCommand(searchMoviesListViewNavigationService);
            GoToAddedMoviesListView = new NavigateCommand(addedMoviesListViewNavigationService);

            MovieName = _movie.Movie.MovieName;
            Description = _movie.Movie.Description;
            Rating = _movie.Movie.Rating;
            Year = _movie.Movie.Year;
            Length = _movie.Movie.Length;
            Genre = _movie.Movie.Genre;
            Directors = _movie.Movie.Directors;
            Writers = _movie.Movie.Writers;

            (_profileMovie, DeleteEnabled) = _moviePage.GetProfileMovie(_profile.CurrentProfile , _movie.Movie);

            SelectedScore = _moviePage.GetSelectedScore(_profileMovie.Score);

            ComboBoxScores = new ObservableCollection<string> { "Select", "(10) Masterpiece", "(9) Great", "(8) Very Good",
                "(7) Good", "(6) Fine", "(5) Average", "(4) Bad", "(3) Very Bad", "(2) Horrible", "(1) Appalling" };
        }

        private readonly Baza _users;
        private readonly ProfileStore _profile;
        private readonly MovieStore _movie;
        private readonly HistoryItem _profileMovie;
        private readonly MoviePage _moviePage = new MoviePage();

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

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; OnPropertyChanged(nameof(Genre)); }
        }

        private string _directors;
        public string Directors
        {
            get { return _directors; }
            set { _directors = value; OnPropertyChanged(nameof(Directors)); }
        }

        private string _writers;
        public string Writers
        {
            get { return _writers; }
            set { _writers = value; OnPropertyChanged(nameof(Writers)); }
        }

        private object _content;
        public object Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(nameof(Content)); }
        }

        private bool _deleteEnabled;
        public bool DeleteEnabled
        {
            get { return _deleteEnabled; }
            set { _deleteEnabled = value; OnPropertyChanged(nameof(DeleteEnabled)); }
        }

        private RelayCommand? _deleteFromListCommand;
        public RelayCommand DeleteFromListCommand
        {
            get
            {
                if (_deleteFromListCommand == null)
                    _deleteFromListCommand = new RelayCommand(argument =>
                    {
                        DeleteEnabled = _moviePage.Delete(_profile.CurrentProfile, _profileMovie, _movie.Movie);
                    }, argument => true);
                return _deleteFromListCommand;
            }
        }

        private RelayCommand? _addToPTWCommand;
        public RelayCommand AddToPTWCommand
        {
            get
            {
                if (_addToPTWCommand == null)
                    _addToPTWCommand = new RelayCommand(argument =>
                    {
                        _moviePage.ExecuteAddToListCommand("PTW", SelectedScore, MovieName, Description, Year, Length, Genre, _profile.CurrentProfile, _movie.Movie, GoToProfileView, _profileMovie);
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
                        _moviePage.ExecuteAddToListCommand("Watched", SelectedScore, MovieName, Description, Year, Length, Genre, _profile.CurrentProfile, _movie.Movie, GoToProfileView, _profileMovie);
                    }, argument => true);
                return _addToWatchedCommand;
            }
        }
    }
}
