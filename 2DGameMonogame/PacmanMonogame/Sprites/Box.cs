using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Sprites
{
    public class Box : Sprite
    {
        private float _timer;

        public Box(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }
       
        }
        public override void OnCollide(Sprite sprite)
        {
            if (sprite == this.Parent)
                return;
            if (sprite is Bullet)
                return;
            if (sprite is Player)
                return;
            if (sprite is Enemy)
                return;

            IsRemoved = true;
        }
    }
}
