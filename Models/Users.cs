using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\users.json");
            string jsonText = File.ReadAllText(fileName);
            _profiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Profile>>(jsonText);
        }

        public void Serialize(List<Profile> profiles)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(profiles);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\users.json"), json);
        }
    }
}
