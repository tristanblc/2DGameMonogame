using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Sprites
{
    public class Player : Sprite
    {
       
        public float Life { get; set; }
        public float Speed { get; set; }

        public bool isDead
        {
            get
            {
                return Life <= 0;
            }
        }


        public Player(Texture2D texture) : base(texture) 
        {
            Speed = 3f;
            Position = new Vector2(1000,1000);
        
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position.X -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Position.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                Position.Y -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += 1;
            }

        }

    }
}
