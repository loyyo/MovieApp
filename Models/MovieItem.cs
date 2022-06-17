﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Models
{
    internal class MovieItem
    {
        public string MovieName { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Year { get; set; }
        public int Length { get; set; }
        public string Genre { get; set; }
        public string Directors { get; set; }
        public string Actors { get; set; }

        public MovieItem(string movieName, string description, int rating, string year, int length, string genre, string directors, string actors)
        {
            MovieName = movieName;
            Description = description;
            Rating = rating;
            Year = year;
            Length = length;
            Genre = genre;
            Directors = directors;
            Actors = actors;
        }

        public override string ToString()
        {
            return $"Movie Name: {MovieName}\nDescription: {Description}\nRating: {Rating}\n" +
                $"Year: {Year}\nLength: {Length}\nGenre: {Genre}\nDirectors: {Directors}\nActors: {Actors}";
        }
    }
}