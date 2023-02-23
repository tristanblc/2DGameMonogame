using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacmanMonogame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Manager
{
    public class WallManager
    {
        private Texture2D _texture;
        private Random _random;

        public WallManager(Texture2D texture)
        {                    
            _texture = texture;
            _random = new Random();
        }

        public List<Sprite> SpawnWall() 
        {
            List<Sprite> list = new List<Sprite>();
            int i = 0;
            while (i < 4)
            {
                var xPos = _random.Next(0, (int)Globals.ScreenWidth + 1);
                var yPos = _random.Next(0, (int)Globals.ScreenHeight + 1);
                list.Add(new PowerUp(_texture)
                {
                    Position = new Vector2(xPos, yPos),
                    HealthUp = 25
                }
                );
                i++;
            }
            return list;
        }
    }
}
