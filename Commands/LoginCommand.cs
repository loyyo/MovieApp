using ProjektProgramowanie.Models;
using ProjektProgramowanie.Services;
using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektProgramowanie.Commands
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
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new List<HistoryItem>());
            bool correct = false;
            Trace.WriteLine(_loginViewModel._users._profiles[0]);

            foreach (Profile user in _loginViewModel._users._profiles)
            {
                if (_loginViewModel.LoginPassword == user.Password &
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
