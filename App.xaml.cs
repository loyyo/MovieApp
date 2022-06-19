using ProjektProgramowanie.Models;
using ProjektProgramowanie.Services;
using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektProgramowanie
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly MovieNavigationStore _movieNavigationStore;
        private readonly ProfileStore _profile;
        private readonly MovieStore _movie;

        private readonly Baza _baza;

        public App()
        {
            _navigationStore = new NavigationStore();
            _movieNavigationStore = new MovieNavigationStore();
            _profile = new ProfileStore();
            _movie = new MovieStore();
            _baza = new Baza();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateLoginRegisterViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _movieNavigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private MoviePageViewModel CreateMoviePageViewModel()
        {
            return new MoviePageViewModel(
                _baza,
                _profile,
                _movie,
                new NavigationService(_navigationStore, CreateProfileViewModel),
                new NavigationService(_navigationStore, CreateAccountSettingsViewModel),
                new NavigationService(_navigationStore, CreateSearchMoviesListViewModel),
                new NavigationService(_navigationStore, CreateAddedMoviesListViewModel));
        }

        private AddedMoviesListViewModel CreateAddedMoviesListViewModel()
        {
            return new AddedMoviesListViewModel(
                _profile,
                _baza,
                _movie,
                new NavigationService(_navigationStore, CreateMoviePageViewModel),
                new NavigationService(_navigationStore, CreateProfileViewModel),
                new NavigationService(_navigationStore, CreateAccountSettingsViewModel),
                new NavigationService(_navigationStore, CreateSearchMoviesListViewModel));
        }

        private AccountSettingsViewModel CreateAccountSettingsViewModel()
        {
            return new AccountSettingsViewModel(
                _baza,
                _profile,
                new NavigationService(_navigationStore, CreateProfileViewModel),
                new NavigationService(_navigationStore, CreateSearchMoviesListViewModel),
                new NavigationService(_navigationStore, CreateAddedMoviesListViewModel));
        }

        private SearchMoviesListViewModel CreateSearchMoviesListViewModel()
        {
            return new SearchMoviesListViewModel(
                _baza,
                _movie,
                new NavigationService(_navigationStore, CreateMoviePageViewModel),
                new NavigationService(_navigationStore, CreateProfileViewModel),
                new NavigationService(_navigationStore, CreateAccountSettingsViewModel),
                new NavigationService(_navigationStore, CreateAddedMoviesListViewModel));
        }

        private ProfileViewModel CreateProfileViewModel()
        {
            return new ProfileViewModel(
                _profile,
                new NavigationService(_navigationStore, CreateAccountSettingsViewModel),
                new NavigationService(_navigationStore, CreateSearchMoviesListViewModel),
                new NavigationService(_navigationStore, CreateAddedMoviesListViewModel));
        }

        private LoginRegisterViewModel CreateLoginRegisterViewModel()
        {
            return new LoginRegisterViewModel(
                _baza,
                _profile,
                new NavigationService(_navigationStore, CreateProfileViewModel));
        }


    }
}
