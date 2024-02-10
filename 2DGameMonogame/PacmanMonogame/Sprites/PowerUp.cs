using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacmanMonogame.Other;
using Sprites;
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

        public float HealthUp { get; set; }

        private float _timer;


        public PowerUp(Texture2D texture) : base(texture)
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
                if (p.Health + this.HealthUp > 100)
                {
                    p.Health = 100;

                }
                else
                {
                    p.Health += this.HealthUp;
                    GlobalsStats.powerUpUsed++;
                }
                IsRemoved = true;
                
            }
            if (sprite is Enemy)
            {
                var enemy = (Enemy)sprite;
                if (enemy.Health + this.HealthUp > 100)
                {
                    enemy.Health = 100;
                    GlobalsStats.powerUpUsed++;
                }
                else
                {
                    enemy.Health += this.HealthUp;
                    GlobalsStats.powerUpUsed++;
                }
            }

            IsRemoved = true;
        }
    }
}
