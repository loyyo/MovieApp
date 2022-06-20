using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    internal interface IMovie
    {
        string MovieName { get; set; }
        string Description { get; set; }
        string Year { get; set; }
        int Length { get; set; }
        string Genre { get; set; }
    }
}
