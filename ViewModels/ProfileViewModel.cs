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
    internal class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel(
            NavigationService accountSettingsViewNavigationService,
            NavigationService searchMoviesListViewNavigationService,
            NavigationService addedMoviesListViewNavigationService)
        {
            GoToAccountSettingsView = new NavigateCommand(accountSettingsViewNavigationService);
            GoToSearchMoviesListView = new NavigateCommand(searchMoviesListViewNavigationService);
            GoToAddedMoviesListView = new NavigateCommand(addedMoviesListViewNavigationService);
        }

        public ICommand GoToAccountSettingsView { get; }
        public ICommand GoToSearchMoviesListView { get; }
        public ICommand GoToAddedMoviesListView { get; }
    }
}