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
using System.Windows;
using System.Windows.Input;

namespace ProjektProgramowanie.ViewModels
{
    internal class MoviePageViewModel : BaseViewModel
    {
        public MoviePageViewModel(
            Users users,
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

            try
            {
                _profileMovie = _profile.CurrentProfile.AddedMovies.Where(x => x.MovieName == _movie.Movie.MovieName).First();
                DeleteEnabled = true;
            }
            catch (Exception ex)
            {
                DeleteEnabled = false;
                _profileMovie = new HistoryItem(string.Empty, string.Empty, 0, string.Empty, 0, string.Empty, string.Empty, string.Empty);
            }

            if (_profileMovie.Score == 0) SelectedScore = "Select";
            else
            {
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
            }

            ComboBoxScores = new ObservableCollection<string> { "Select", "(10) Masterpiece", "(9) Great", "(8) Very Good",
                "(7) Good", "(6) Fine", "(5) Average", "(4) Bad", "(3) Very Bad", "(2) Horrible", "(1) Appalling" };
        }

        private readonly Users _users;
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
                        _profile.CurrentProfile.AddedMovies.Remove(_profileMovie);
                        _users.Serialize(_users._profiles);
                        MessageBox.Show("Movie successfully deleted from list", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        DeleteEnabled = false;
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
                        ExecuteAddToListCommand("PTW");
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
                        ExecuteAddToListCommand("Watched");
                    }, argument => true);
                return _addToWatchedCommand;
            }
        }
        public void ExecuteAddToListCommand(string ListName)
        {
            int YourScore = 0;
            switch (SelectedScore)
            {
                case "Select":
                    YourScore = 0;
                    break;
                case "(1) Appalling":
                    YourScore = 1;
                    break;
                case "(2) Horrible":
                    YourScore = 2;
                    break;
                case "(3) Very Bad":
                    YourScore = 3;
                    break;
                case "(4) Bad":
                    YourScore = 4;
                    break;
                case "(5) Average":
                    YourScore = 5;
                    break;
                case "(6) Fine":
                    YourScore = 6;
                    break;
                case "(7) Good":
                    YourScore = 7;
                    break;
                case "(8) Very Good":
                    YourScore = 8;
                    break;
                case "(9) Great":
                    YourScore = 9;
                    break;
                case "(10) Masterpiece":
                    YourScore = 10;
                    break;
            }

            HistoryItem historyItem = new HistoryItem(
                MovieName,
                Description,
                YourScore,
                Year,
                Length,
                Genre,
                ListName,
                DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString()
                );

            if (YourScore != 0 & !_profile.CurrentProfile.AddedMovies.Contains(_profileMovie))
            {
                _profile.CurrentProfile.AddedMovies.Add(historyItem);
                _users.Serialize(_users._profiles);
                MessageBox.Show("Movie successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                GoToProfileView.Execute(this);
            }

            else if (YourScore != 0 & _profile.CurrentProfile.AddedMovies.Contains(_profileMovie))
            {
                _profile.CurrentProfile.AddedMovies.Remove(_profileMovie);
                _profile.CurrentProfile.AddedMovies.Add(historyItem);
                _users.Serialize(_users._profiles);
                MessageBox.Show("Score successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                GoToProfileView.Execute(this);
            }
        }
    }
}
