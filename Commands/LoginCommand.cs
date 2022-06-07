using ProjektProgramowanie.Services;
using ProjektProgramowanie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Commands
{
    internal class LoginCommand : CommandBase
    {
        private readonly NavigationService _profileViewNavigationService;

        public LoginCommand(LoginRegisterViewModel loginViewModel, NavigationService profileViewNavigationService)
        {
            _profileViewNavigationService = profileViewNavigationService;
        }

        public override void Execute(object parameter)
        {
            _profileViewNavigationService.Navigate();
        }
    }
}
