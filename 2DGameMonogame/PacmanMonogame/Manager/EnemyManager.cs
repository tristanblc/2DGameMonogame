using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacmanMonogame.Sprites;
using Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Manager
{
    public class EnemyManager
    {
        private Random _random;
        private Texture2D _texture;
        private Texture2D _textureBullet;
        private Player _player;
        public EnemyManager(Texture2D texture,Texture2D textureBullet,Player player) 
        {
            _random = new Random();
            _texture = texture;    
            _textureBullet = textureBullet;
            _player = player;
        }
        public List<Sprite> SpawnEnemies()
        {
            List<Sprite> list = new List<Sprite>();
            int i = 0;
            while(i < 10) 
            {
                var xPos = _random.Next(0, 1800);
                var yPos = _random.Next(0, 1800);
                list.Add(new Enemy(_texture)
                {
                    Position = new Vector2(xPos, yPos),
                    FollowTarget = _player,
                    FollowDistance = 1000f,
                    Bullet = new Bullet(_textureBullet)
                }
                );
                i++;
            }
            return list;
        }

    }
}
