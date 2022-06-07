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
    }
}
