using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Models
{
    internal class HistoryItem
    {
        public string MovieName { get; }
        public string Description { get; }
        public int Score { get; }
        public string Year { get; }
        public int Length { get; }
        public string Genre { get; }
        public string List { get; }
        public string Date { get; }

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
