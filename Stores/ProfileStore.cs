using ProjektProgramowanie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Stores
{
    internal class ProfileStore
    {
        private Profile _currentProfile;
        public Profile CurrentProfile
        {
            get => _currentProfile;
            set { _currentProfile = value; }
        }
    }
}
