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

        public Service()
        {
            string workingDirectory = Environment.CurrentDirectory;

            var path = Path.Combine(workingDirectory, "Save");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            _path = path;
        }

        public List<KeyData> ReadSavedKeys()
        {
            var name = $"ButtonSave.json";
            List<KeyData> items = new List<KeyData>();
            if (File.Exists(Path.Join(_path, name)))
            {
                using (StreamReader r = new StreamReader(Path.Join(_path, name)))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<KeyData>>(json);
                }
            }
            return items;
        }

        public List<Button> ReadSavedKeysMenu(List<Button> buttons)
        {
           int i = 0;
           var items = this.ReadSavedKeys();
           while (i< buttons.Count)
            {
                foreach(var item in items)
                {
                    if (item.ButtonName == buttons[i].Name)
                        buttons[i].Text = item.Key;
                }
                i++;
            }

            return buttons;


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

            var name = $"ButtonSave.json";

            if(!File.Exists(Path.Join(_path, name)))
                File.Create(Path.Join(_path, name)).Close();
      
                       

            File.WriteAllText(Path.Join(_path,name),json);
          

        }
    }
}
