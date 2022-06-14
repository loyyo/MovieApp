using ProjektProgramowanie.Commands;
using ProjektProgramowanie.Services;
using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektProgramowanie.ViewModels
{
    internal class LoginRegisterViewModel : BaseViewModel
    {
        public LoginRegisterViewModel(NavigationService profileViewNavigationService)
        {
            LoginCommand = new LoginCommand(this, profileViewNavigationService);
            RegisterCommand = new RegisterCommand(this, profileViewNavigationService);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private string _loginUsernameOrEmail;
        public string LoginUsernameOrEmail
        {
            get { return _loginUsernameOrEmail; }
            set { _loginUsernameOrEmail = value; OnPropertyChanged(nameof(LoginUsernameOrEmail)); }
        }

        private string _loginPassword;
        public string LoginPassword
        {
            get { return _loginPassword; }
            set { _loginPassword = value; OnPropertyChanged(nameof(LoginPassword)); }
        }

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
    }
}
