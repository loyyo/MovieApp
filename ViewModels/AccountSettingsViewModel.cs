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
    internal class AccountSettingsViewModel : BaseViewModel
    {
        public AccountSettingsViewModel(
            NavigationService profileViewNavigationService,
            NavigationService searchMoviesListViewNavigationService,
            NavigationService addedMoviesListViewNavigationService)
        {
            GoToProfileView = new NavigateCommand(profileViewNavigationService);
            GoToSearchMoviesListView = new NavigateCommand(searchMoviesListViewNavigationService);
            GoToAddedMoviesListView = new NavigateCommand(addedMoviesListViewNavigationService);
        }

        public ICommand GoToProfileView { get; }
        public ICommand GoToSearchMoviesListView { get; }
        public ICommand GoToAddedMoviesListView { get; }
    }
}
