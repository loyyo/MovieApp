using ProjektProgramowanie.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Services
{
    internal class CloseMovieNavigationService : INavigationService
    {
        private readonly MovieNavigationStore _navigationStore;

        public CloseMovieNavigationService(MovieNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            _navigationStore.Close();
        }
    }
}
