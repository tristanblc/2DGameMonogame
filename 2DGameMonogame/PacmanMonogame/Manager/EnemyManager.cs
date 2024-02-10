using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        private SoundEffect _hitSong;
        private SoundEffect _shootSong;
        public EnemyManager(Texture2D texture,Texture2D textureBullet,Player player,ContentManager content) 
        {
            _random = new Random();
            _texture = texture;    
            _textureBullet = textureBullet;
            _player = player;
            _hitSong = content.Load<SoundEffect>("hit");
            _shootSong = content.Load<SoundEffect>("shoot");
        }
        public List<Sprite> SpawnEnemies(int number)
        {
            List<Sprite> list = new List<Sprite>();
            int i = 0;
            while(i < _random.Next(1,5)) 
            {
                var xPos = _random.Next(0, (int)Globals.ScreenWidth + 1);
                var yPos = _random.Next(0, (int)Globals.ScreenHeight + 1);
                list.Add(new Enemy(_texture,_shootSong)
                {
                    Position = new Vector2(xPos, yPos),
                    FollowTarget = _player,
                    FollowDistance = 1000f,
                    Bullet = new Bullet(_textureBullet,_hitSong)
                }
                );
                i++;
            }
            return list;
        }

    }
}
