using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    internal class HistoryItem : IMovie
    {
        public string MovieName { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public string Year { get; set; }
        public int Length { get; set; }
        public string Genre { get; set; }
        public string List { get; set; }
        public string Date { get; set; }

        public HistoryItem(string movieName, string description, int score, string year, int length, string genre, string list, string date)
        {
            MovieName = movieName;
            Description = description;
            Score = score;
            Year = year;
            Length = length;
            Genre = genre;
            List = list;
            Date = date;
        }

        public override string ToString()
        {
            return $"Movie Name: {MovieName}\nDescription: {Description}\nScore: {Score}\n" +
                $"Year: {Year}\nLength: {Length}\nGenre: {Genre}\nList: {List}\nDate: {Date}";
        }
    }
}
