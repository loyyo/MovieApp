using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Models
{
    internal class Users
    {
        public List<Profile> _profiles { get; set; }

        public Users()
        {
            string fileName = @"C:\Users\Maciek\source\repos\ProjektProgramowanie\Resources\users.json";
            string jsonText = File.ReadAllText(fileName);
            _profiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Profile>>(jsonText);
        }
    }
}
