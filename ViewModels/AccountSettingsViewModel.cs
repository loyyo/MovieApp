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

namespace MovieApp.ViewModels
{
    internal class AccountSettingsViewModel : BaseViewModel
    {
        public Baza _users;
        public AccountSettingsViewModel(
            Baza users,
            ProfileStore profileStore,
            NavigationService profileViewNavigationService,
            NavigationService searchMoviesListViewNavigationService,
            NavigationService addedMoviesListViewNavigationService)
        {
            GoToProfileView = new NavigateCommand(profileViewNavigationService);
            GoToSearchMoviesListView = new NavigateCommand(searchMoviesListViewNavigationService);
            GoToAddedMoviesListView = new NavigateCommand(addedMoviesListViewNavigationService);
            ChangeProfileCommand = new ChangeProfileCommand(this, profileStore, profileViewNavigationService);
            _users = users;
            FirstName = string.Empty;
            Surname = string.Empty;
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Birthday = DateTime.Today;
            Country = string.Empty;
            Description = string.Empty;
        }

        public ICommand ChangeProfileCommand { get; }
        public ICommand GoToProfileView { get; }
        public ICommand GoToSearchMoviesListView { get; }
        public ICommand GoToAddedMoviesListView { get; }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; OnPropertyChanged(nameof(Birthday)); }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(nameof(Country)); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }
    }
}
