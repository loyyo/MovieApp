using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Stores;
using MovieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static MovieApp.Services.PasswordToHashConverter;

namespace MovieApp.Commands
{
    internal class LoginCommand : CommandBase
    {
        private readonly LoginRegisterViewModel _loginViewModel;
        private readonly NavigationService _profileViewNavigationService;
        private readonly ProfileStore _profileStore;

        public LoginCommand(LoginRegisterViewModel loginViewModel, ProfileStore profileStore, NavigationService profileViewNavigationService)
        {
            _loginViewModel = loginViewModel;
            _profileStore = profileStore;
            _profileViewNavigationService = profileViewNavigationService;
        }

        public override void Execute(object parameter)
        {

            Profile profil = new Profile(string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new List<HistoryItem>(), 0);
            bool correct = false;

            foreach (Profile user in _loginViewModel._users._profiles)
            {
                    if (SecurePasswordHasher.Verify(_loginViewModel.LoginPassword, user.Password) &
                    (_loginViewModel.LoginUsernameOrEmail == user.Username | _loginViewModel.LoginUsernameOrEmail == user.Email))
                    { correct = true; profil = user; }
            }

            if (correct)
            {
                MessageBox.Show("Login Successful :)", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _profileStore.CurrentProfile = profil;
                _profileViewNavigationService.Navigate();
            }
            else MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
