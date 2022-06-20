using ProjektProgramowanie.DAL.Entities;
using ProjektProgramowanie.DAL.Repositories;
using ProjektProgramowanie.Models;
using ProjektProgramowanie.Services;
using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ProjektProgramowanie.Services.PasswordToHashConverter;

namespace ProjektProgramowanie.Commands
{
    internal class RegisterCommand : CommandBase
    {
        private readonly LoginRegisterViewModel _registerViewModel;
        private readonly ProfileStore _profileStore;
        private readonly NavigationService _profileViewNavigationService;

        public RegisterCommand(LoginRegisterViewModel registerViewModel, ProfileStore profileStore, NavigationService profileViewNavigationService)
        {
            _registerViewModel = registerViewModel;
            _profileStore = profileStore;
            _profileViewNavigationService = profileViewNavigationService;
        }

        public override void Execute(object parameter)
        {
            bool exists = false;

            Profile profil = new Profile(
                _registerViewModel.FirstName,
                _registerViewModel.Surname,
                _registerViewModel.Username,
                _registerViewModel.Email,
                _registerViewModel.Password,
                _registerViewModel.Birthday.ToShortDateString(),
                _registerViewModel.Country,
                "Tutaj jest opis",
                new List<HistoryItem>(),
                0);

            if (_registerViewModel.FirstName == null | _registerViewModel.FirstName == string.Empty)
            {
                MessageBox.Show("Please enter your name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_registerViewModel.Surname == null | _registerViewModel.Surname == string.Empty)
            {
                MessageBox.Show("Please enter your surname.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_registerViewModel.Username == null | _registerViewModel.Username.Length <= 2)
            {
                MessageBox.Show("Your username should have at least 3 characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_registerViewModel.Email == null | !_registerViewModel.Email.Contains("@") | !_registerViewModel.Email.Contains(".") | _registerViewModel.Email.Length <= 5)
            {
                MessageBox.Show("Please enter a valid email address", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_registerViewModel.Password == null | !_registerViewModel.Password.Any(char.IsDigit) | !_registerViewModel.Password.Any(char.IsUpper) | _registerViewModel.Password.Length <= 4)
            {
                MessageBox.Show("Password must have a number, upper case letter and at least 5 characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_registerViewModel.Country == null | _registerViewModel.Country == string.Empty)
            {
                MessageBox.Show("Please enter your country.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (Profile user in _registerViewModel._users._profiles)
            {
                if (_registerViewModel.Username == user.Username | _registerViewModel.Email == user.Email)
                { exists = true; }
            }

            if (!exists)
            {
                var hash = SecurePasswordHasher.Hash(profil.Password);
                profil.Password = hash;

                var auth = new Auth(profil.Username, profil.Email, hash);
                if (RepositoryAuth.AddAuthToDB(auth))
                {
                    string[] splitBirthday = profil.Birthday.Split(".");
                    string correctBirthday = splitBirthday[2] + "-" + splitBirthday[1] + "-" + splitBirthday[0];

                    int id_auth = RepositoryAuth.GetAllAuth().OrderByDescending(i => i.Id).First().Id;
                    var user = new User(id_auth, profil.Name, profil.Surname, profil.Description, correctBirthday, profil.Country);
                    if (RepositoryUser.AddUserToDB(user))
                    {;
                        profil.ID = RepositoryUser.GetAllUsers().OrderByDescending(u => u.Id).First().Id;
                        _profileStore.CurrentProfile = profil;
                        _profileViewNavigationService.Navigate();
                        MessageBox.Show("Registration Successful :)", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else if (exists) MessageBox.Show("A user with this email/username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
