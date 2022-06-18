using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Models
{
    internal class Movies
    {
        public List<MovieItem> _moviesList { get; set; }

        public Movies()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\movies.json");
            string jsonText = File.ReadAllText(fileName);
            //System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            //culture.NumberFormat.NumberDecimalSeparator = ".";
            //, new Newtonsoft.Json.JsonSerializerSettings() { Culture = culture }
            _moviesList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MovieItem>>(jsonText);
        }
    }
}
