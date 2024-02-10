using PäcmanMonogame.Controls;
using PacmanMonogame.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Services
{
    public interface IService
    {
        void SaveKeyInJson(List<Button> buttons);

        List<KeyData> ReadSavedKeys();

        List<Button> ReadSavedKeysMenu(List<Button> buttons);

        void SaveStats(SaveGame savedGame);

        SaveGame ReadSave();
    }
}
