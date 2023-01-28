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
    public class MegaPowerUp : Sprite
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public float MegaGain { get; set; }

        private float _timer;


        public MegaPowerUp(Texture2D texture) : base(texture)
        {
            LifeSpan = 2f;
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
            {
                var p = (Player)sprite;

                if(p.CooldownMega - p.ShootCounterMega   < 5)
                {

                    p.ShootCounterMega--;
                }            
            
                IsRemoved = true;

            }
            if (sprite is Enemy)
            {
                return;
            }

            IsRemoved = true;
        }
    }
    
}
