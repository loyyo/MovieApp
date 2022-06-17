using ProjektProgramowanie.Commands;
using ProjektProgramowanie.Models;
using ProjektProgramowanie.Services;
using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektProgramowanie.ViewModels
{
    internal class SearchMoviesListViewModel : BaseViewModel
    {
        public SearchMoviesListViewModel(
            MoviesStore moviesStore,
            NavigationService profileViewNavigationService,
            NavigationService accountSettingsViewNavigationService,
            NavigationService addedMoviesListViewNavigationService)
        {
            _movies = moviesStore;
            GoToProfileView = new NavigateCommand(profileViewNavigationService);
            GoToAccountSettingsView = new NavigateCommand(accountSettingsViewNavigationService);
            GoToAddedMoviesListView = new NavigateCommand(addedMoviesListViewNavigationService);
        }

        private readonly MoviesStore _movies;
        public ICommand GoToProfileView { get; }
        public ICommand GoToAccountSettingsView { get; }
        public ICommand GoToAddedMoviesListView { get; }
    }
}
