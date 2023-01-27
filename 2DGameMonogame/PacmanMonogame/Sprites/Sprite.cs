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
    public class Sprite : ICloneable
    {

        public Texture2D _texture;

        public Vector2 Position;
        public Vector2 Direction;
        public Vector2 Origin { get; set; } = new Vector2(0,0);

        protected KeyboardState currentKey;
        protected KeyboardState previousKey;


        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;
        public float _rotation;
        public float LifeSpan { get; set; }
        public float direction;


        public Sprite Parent;

        public bool IsRemoved = false;



        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

   

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public object Clone()
        {
           return this.MemberwiseClone();
        }
    }
}
