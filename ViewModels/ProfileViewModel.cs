using ProjektProgramowanie.Commands;
using ProjektProgramowanie.Models;
using ProjektProgramowanie.Services;
using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ProjektProgramowanie.ViewModels
{
    internal class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel(
            ProfileStore profileStore,
            NavigationService accountSettingsViewNavigationService,
            NavigationService searchMoviesListViewNavigationService,
            NavigationService addedMoviesListViewNavigationService)
        {
            _profile = profileStore;
            GoToAccountSettingsView = new NavigateCommand(accountSettingsViewNavigationService);
            GoToSearchMoviesListView = new NavigateCommand(searchMoviesListViewNavigationService);
            GoToAddedMoviesListView = new NavigateCommand(addedMoviesListViewNavigationService);
            MoviesPTW = 0;
            MoviesWatched = 0;
            MinutesWatched = 0;
            AverageRating = 0;
            foreach (var movie in _profile.CurrentProfile.AddedMovies)
            {
                if (movie.List == "Watched") { MoviesWatched++; MinutesWatched += movie.Length; }
                if (movie.List == "PTW") MoviesPTW++;
                AverageRating += movie.Score;
            }
            AverageRating /= (MoviesWatched + MoviesPTW);
            HistoryList = new ObservableCollection<ProfileHistoryViewModel>();
            foreach (HistoryItem movie in AddedMovies)
            {
                HistoryList.Add(new ProfileHistoryViewModel(movie.MovieName, movie.Description, movie.Score, movie.Year, movie.Length, movie.Genre, movie.List, movie.Date));
            }
        }

        private readonly ProfileStore _profile;

        public ICommand GoToAccountSettingsView { get; }
        public ICommand GoToSearchMoviesListView { get; }
        public ICommand GoToAddedMoviesListView { get; }

        public string Email => _profile.CurrentProfile.Email;
        public string Password => _profile.CurrentProfile.Password;

        public string Username => _profile.CurrentProfile.Username;
        public string Picture => _profile.CurrentProfile.Picture;
        public string Name => _profile.CurrentProfile.Name;
        public string Surname => _profile.CurrentProfile.Surname;
        public string Birthday => _profile.CurrentProfile.Birthday;
        public string Country => _profile.CurrentProfile.Country;

        public List<HistoryItem> AddedMovies => _profile.CurrentProfile.AddedMovies;

        private int _moviesPTW;
        public int MoviesPTW
        {
            get { return _moviesPTW; }
            set { _moviesPTW = value; OnPropertyChanged(nameof(MoviesPTW)); }
        }

        private int _moviesWatched;
        public int MoviesWatched
        {
            get { return _moviesWatched; }
            set { _moviesWatched = value; OnPropertyChanged(nameof(MoviesWatched)); }
        }

        private int _minutesWatched;
        public int MinutesWatched
        {
            get { return _minutesWatched; }
            set { _minutesWatched = value; OnPropertyChanged(nameof(MinutesWatched)); }
        }

        private float _averageRating;
        public float AverageRating
        {
            get { return _averageRating; }
            set { _averageRating = value; OnPropertyChanged(nameof(AverageRating)); }
        }

        private ObservableCollection<ProfileHistoryViewModel> _historyList;
        public ObservableCollection<ProfileHistoryViewModel> HistoryList
        {
            get { return _historyList; }
            set { _historyList = value; OnPropertyChanged(nameof(HistoryList)); }
        }
    }
}
