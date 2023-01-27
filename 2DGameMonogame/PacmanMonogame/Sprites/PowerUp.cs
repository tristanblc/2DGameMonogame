using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Sprites
{
    public class PowerUp : Sprite
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public float Level { get; set; }


        public PowerUp(Texture2D texture) : base(texture)
        {
        
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
         

        }
    }
}
