using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly MovieNavigationStore _movieNavigationStore;

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public BaseViewModel CurrentMovieViewModel => _movieNavigationStore.CurrentViewModel;
        public bool IsOpen => _movieNavigationStore.IsOpen;

        public MainViewModel(NavigationStore navigationStore, MovieNavigationStore movieNavigationStore)
        {
            _navigationStore = navigationStore;
            _movieNavigationStore = movieNavigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _movieNavigationStore.CurrentViewModelChanged += OnCurrentMovieViewModelChanged;
        }

        private void OnCurrentMovieViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentMovieViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
