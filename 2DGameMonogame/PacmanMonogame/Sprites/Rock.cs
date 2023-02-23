using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Sprites
{
    public class Rock : Sprite
    { 
        public Rock(Texture2D texture) : base(texture)
        {
        
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {


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
                if(p.Position == this.Position)
                {
                    if (p.Position.X >= Globals.ScreenWidth - 3)
                        p.Position.Y++;
                    if (Position.Y < 2)
                        p.Position.Y = 5;
                    if (Position.X < 2)
                        p.Position.X = 5;
                    if (Position.Y > Globals.ScreenHeight - 3)
                        p.Position.X++;
                 
                }
                    
                

            }
            if (sprite is Enemy)
            {
                var p = (Enemy)sprite;
                if (p.Position == this.Position)
                {
                    if (p.Position.X >= Globals.ScreenWidth - 3)
                        p.Position.Y++;
                    if (Position.Y < 2)
                        p.Position.Y = 5;
                    if (Position.X < 2)
                        p.Position.X = 5;
                    if (Position.Y > Globals.ScreenHeight - 3)
                        p.Position.X++;

                }
                return;
            }
            if(sprite is Bullet)
            {
                var bullet = sprite as Bullet;
                bullet.IsRemoved = true;
            }
              


            IsRemoved = true;
        }
    }
}
