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
    internal class AddedMoviesListViewModel : BaseViewModel
    {
        public AddedMoviesListViewModel(
            NavigationService profileViewNavigationService,
            NavigationService accountSettingsViewNavigationService,
            NavigationService searchMoviesListViewNavigationService)
        {
            GoToProfileView = new NavigateCommand(profileViewNavigationService);
            GoToAccountSettingsView = new NavigateCommand(accountSettingsViewNavigationService);
            GoToSearchMoviesListView = new NavigateCommand(searchMoviesListViewNavigationService);
        }

        public ICommand GoToProfileView { get; }
        public ICommand GoToAccountSettingsView { get; }
        public ICommand GoToSearchMoviesListView { get; }
    }
}
