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
    public class MegaPowerUpManager
    {
        private Random _random;
        private Texture2D _texture;
        public MegaPowerUpManager(Texture2D texture)
        {
            _random = new Random();
            _texture = texture;
        }
        public List<Sprite> SpawnPowerUps()
        {
            List<Sprite> list = new List<Sprite>();
            int i = 0;
            while (i < 2)
            {
                var xPos = _random.Next(0, (int)Globals.ScreenWidth + 1);
                var yPos = _random.Next(0, (int)Globals.ScreenHeight + 1);
                list.Add(new MegaPowerUp(_texture)
                {
                    Position = new Vector2(xPos, yPos),
                    MegaGain = 2
                }
                );
                i++;
            }
            return list;
        }
    }
}
