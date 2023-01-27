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
    public class PowerUpManager
     {

        private Random _random;
        private Texture2D _texture;
        public PowerUpManager(Texture2D texture)
        {
            _random = new Random();
            _texture = texture;
        }
        public List<Sprite> SpawnPowerUps()
        {
            List<Sprite> list = new List<Sprite>();
            int i = 0;
            while (i < 5)
            {
                var xPos = _random.Next(0, 1800);
                var yPos = _random.Next(0, 1800);
                list.Add(new PowerUp(_texture)
                {
                    Position = new Vector2(xPos, yPos)
                }
                );
                i++;
            }
            return list;
        }
    }
}
