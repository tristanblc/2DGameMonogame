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
    public class Bullet : Sprite
    {

        private float _timer;
        
        public Bullet(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_timer > LifeSpan)
            {
                IsRemoved = true;
            }
            Position += Direction * LinearVelocity;
        }
        public override void OnCollide(Sprite sprite)
        {
            if (sprite == this.Parent)
                return;

          
            if (sprite is Bullet)
                return;

            if(sprite is Player)
            { 
              var p = (Player)sprite;
              p.Health -= 15;
            }
            if(sprite is Enemy)
            {
                var enemy = (Enemy)sprite;
                enemy.Health -= 20;
                if(enemy.Health < 0)
                {
                    enemy.IsRemoved = true;
                    GlobalsStats.enemyKilled++;
                }
                    
            }

            IsRemoved = true;
        }
    }
}
