﻿using MovieApp.Models;
using MovieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Stores
{
    internal class MovieStore
    {
        private MovieItem _movie;
        public MovieItem Movie
        {
            get => _movie;
            set { _movie = value; }
        }
    }
}
