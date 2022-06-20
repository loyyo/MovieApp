using MovieApp.Commands;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Stores;
using MovieApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MovieApp.ViewModels
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

            int MoviesRated = 0;
            foreach (var movie in AddedMovies)
            {
                if (movie.List == "Watched") { MoviesWatched++; MinutesWatched += movie.Length; }
                if (movie.List == "PTW") MoviesPTW++;
                if (movie.Score != 0) MoviesRated++;
                AverageRating += movie.Score;
            }

            if (AverageRating > 0) AverageRating /= MoviesRated;
            HistoryList = new ObservableCollection<ProfileHistoryViewModel>();
            
            _profile.CurrentProfile.FillProfileHistory(_profile.CurrentProfile, HistoryList);
        }

        private readonly ProfileStore _profile;

        public ICommand GoToAccountSettingsView { get; }
        public ICommand GoToSearchMoviesListView { get; }
        public ICommand GoToAddedMoviesListView { get; }

        public string Email => _profile.CurrentProfile.Email;
        public string Password => _profile.CurrentProfile.Password;

        public string Username => _profile.CurrentProfile.Username;
        public string Description => _profile.CurrentProfile.Description;
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
