using ProjektProgramowanie.DAL.Entities;
using ProjektProgramowanie.DAL.Repositories;
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
using static ProjektProgramowanie.Services.PasswordToHashConverter;

namespace ProjektProgramowanie.Commands
{
    internal class ChangeProfileCommand : CommandBase
    {
        private readonly AccountSettingsViewModel _accountSettingsViewModel;
        private readonly ProfileStore _profileStore;
        private readonly NavigationService _profileViewNavigationService;

        public ChangeProfileCommand(AccountSettingsViewModel accountSettingsViewModel, ProfileStore profileStore, NavigationService profileViewNavigationService)
        {
            _accountSettingsViewModel = accountSettingsViewModel;
            _profileStore = profileStore;
            _profileViewNavigationService = profileViewNavigationService;
        }

        public override void Execute(object parameter)
        {
            bool exists = false;
            bool authChanged = false;
            bool userChanged = false;

            string Name = string.Empty;
            string Surname = string.Empty;
            string Username = string.Empty;
            string Email = string.Empty;
            string Password = string.Empty;
            string Birthday = string.Empty;
            string Country = string.Empty;
            string Description = string.Empty;

            if (_accountSettingsViewModel.FirstName == null | _accountSettingsViewModel.FirstName == string.Empty) Name = _profileStore.CurrentProfile.Name;
            if (_accountSettingsViewModel.FirstName != null & _accountSettingsViewModel.FirstName != string.Empty)
            {
                Name = _accountSettingsViewModel.FirstName;
                userChanged = true;
            }

            if (_accountSettingsViewModel.Surname == null | _accountSettingsViewModel.Surname == string.Empty) Surname = _profileStore.CurrentProfile.Surname;
            if (_accountSettingsViewModel.Surname != null & _accountSettingsViewModel.Surname != string.Empty)
            {
                Surname = _accountSettingsViewModel.Surname;
                userChanged = true;
            };

            if (_accountSettingsViewModel.Username == null | _accountSettingsViewModel.Username == string.Empty) Username = _profileStore.CurrentProfile.Username;
            if (_accountSettingsViewModel.Username != null & _accountSettingsViewModel.Username != string.Empty)
            {
                Username = _accountSettingsViewModel.Username;
                authChanged = true;
            }

            if (_accountSettingsViewModel.Email == null | _accountSettingsViewModel.Email == string.Empty) Email = _profileStore.CurrentProfile.Email;
            if (_accountSettingsViewModel.Email != null & _accountSettingsViewModel.Email != string.Empty)
            {
                Email = _accountSettingsViewModel.Email;
                authChanged = true;
            }

            if (_accountSettingsViewModel.Password == null | _accountSettingsViewModel.Password == string.Empty) Password = _profileStore.CurrentProfile.Password;
            if (_accountSettingsViewModel.Password != null & _accountSettingsViewModel.Password != string.Empty)
            {
                Password = _accountSettingsViewModel.Password;
                authChanged = true;
            }

            if (_accountSettingsViewModel.Birthday.ToShortDateString() == DateTime.Today.ToShortDateString()) Birthday = _profileStore.CurrentProfile.Birthday;
            if (_accountSettingsViewModel.Birthday.ToShortDateString() != DateTime.Today.ToShortDateString())
            {
                Birthday = _accountSettingsViewModel.Birthday.ToShortDateString();
                userChanged = true;
            }

            if (_accountSettingsViewModel.Country == null | _accountSettingsViewModel.Country == string.Empty) Country = _profileStore.CurrentProfile.Country;
            if (_accountSettingsViewModel.Country != null & _accountSettingsViewModel.Country != string.Empty)
            {
                Country = _accountSettingsViewModel.Country;
                userChanged = true;
            }

            if (_accountSettingsViewModel.Description == null | _accountSettingsViewModel.Description == string.Empty) Description = _profileStore.CurrentProfile.Description;
            if (_accountSettingsViewModel.Description != null & _accountSettingsViewModel.Description != string.Empty)
            {
                Description = _accountSettingsViewModel.Description;
                userChanged = true;
            }

            Profile profil = new Profile(
                Name,
                Surname,
                Username,
                Email,
                Password,
                Birthday,
                Country,
                Description,
                _profileStore.CurrentProfile.AddedMovies,
                _profileStore.CurrentProfile.ID);

            if (Username.Length <= 2)
            {
                MessageBox.Show("Your username should have at least 3 characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Email.Contains("@") | !Email.Contains(".") | Email.Length <= 5)
            {
                MessageBox.Show("Please enter a valid email address", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Password.Any(char.IsDigit) | !Password.Any(char.IsUpper) | Password.Length <= 4)
            {
                MessageBox.Show("Password must have a number, upper case letter and at least 5 characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (Profile user in _accountSettingsViewModel._users._profiles)
            {
                if (_accountSettingsViewModel.Username == user.Username | _accountSettingsViewModel.Email == user.Email)
                { exists = true; }
            }

            if (!exists)
            {
                bool updated = false;
                int ID = _profileStore.CurrentProfile.ID;

                if (authChanged)
                {
                    var hash = SecurePasswordHasher.Hash(profil.Password);
                    profil.Password = hash;
                    Auth newAuth = new Auth(profil.Username, profil.Email, hash);
                    if(RepositoryAuth.EditAuthInDB(newAuth, ID)) updated = true;
                }
                if(userChanged)
                {
                    string[] splitBirthday = profil.Birthday.Split(".");
                    string correctBirthday = splitBirthday[2] + "-" + splitBirthday[1] + "-" + splitBirthday[0];

                    User newUser = new User(0, profil.Name, profil.Surname, profil.Description, correctBirthday, profil.Country);
                    if (RepositoryUser.EditUserInDB(newUser, ID)) updated = true;
                }

                if(updated)
                {
                    _profileStore.CurrentProfile = profil;
                    MessageBox.Show("Profile successfully changed :)", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    _profileViewNavigationService.Navigate();
                }

            }
            else if (exists) MessageBox.Show("A user with this email/username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
