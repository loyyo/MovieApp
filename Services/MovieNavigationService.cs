using ProjektProgramowanie.Stores;
using ProjektProgramowanie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Services
{
    internal class MovieNavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {
        private readonly MovieNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public MovieNavigationService(MovieNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
