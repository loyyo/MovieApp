using ProjektProgramowanie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Stores
{
    internal class MoviesStore
    {
        private List<MovieItem> _movieList;
        public List<MovieItem> MovieList
        {
            get => _movieList;
            set { _movieList = value; }
        }
    }
}
