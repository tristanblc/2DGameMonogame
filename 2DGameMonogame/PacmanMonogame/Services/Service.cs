using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using PäcmanMonogame.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Services
{
    public class Service : IService
    {
        private string _path;

        public Service(string path)
        {
            _path = path;
        }


        public void SaveKeyInJson(List<Button> buttons)
        {

            List<KeyData> _data = new List<KeyData>();
            buttons.ForEach(button =>
            {
                _data.Add(new KeyData()
                {
                    Key = button.Text,
                    ButtonName = button.Name

                }
                );

            });

            string json = JsonConvert.SerializeObject(_data.ToArray());

            var name = $"save.json";

            if(!File.Exists(Path.Combine(_path, name)))
                File.Create(Path.Combine(_path, name));

            File.WriteAllTextAsync(Path.Combine(_path,name),json);

        }
    }
}
