using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PäcmanMonogame.Controls;
using PacmanMonogame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.States
{
    public class HighScoreState : State
    {
        private Jeu _game;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;
        private List<Button> _components;
        private Texture2D _texture;
        private SpriteFont _font;
        private SaveGame _saved;

        private IService _service;

        public HighScoreState(Jeu game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _game= game;
            _graphicsDevice= graphicsDevice;
            _content= content;
            _texture = _content.Load<Texture2D>("Towel");



            var buttonTexture = _content.Load<Texture2D>("Button");
            _font = _content.Load<SpriteFont>("Font");




            var GoBackButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(800, 400),
                Text = "Go the menu",
            };
            GoBackButton.Click += Button_GoBackButton_Click;

            _components = new List<Button>()
            {
                GoBackButton 
            };


            _service = new Service();


            SaveGame findSave = _service.ReadSave();
            if (_service == null )
                _saved = new SaveGame();
            else
                _saved = findSave;

        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            var backgroundTexture = _content.Load<Texture2D>("Towel");
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, (int)Globals.ScreenWidth, (int)Globals.ScreenHeight), Color.White);

            spriteBatch.Draw(_texture, new Vector2(650, 100), Color.White);

            spriteBatch.DrawString(_font, $"Enemy kill number :{_saved.enemyKilled}", new Vector2(200, 200), Color.Black);
            spriteBatch.DrawString(_font, $"Number of box killed: {_saved.boxKilled}", new Vector2(500, 200), Color.Black);
            spriteBatch.DrawString(_font, $"UpKey Pressed Number  :{_saved.upKeyPressed}", new Vector2(200, 400), Color.Black);
            spriteBatch.DrawString(_font, $"Megapower used :{_saved.megaPowerUpUsed}", new Vector2(500, 400), Color.Black);
            spriteBatch.DrawString(_font, $"Power up used: {_saved.powerUpUsed}", new Vector2(200, 600), Color.Black);
            spriteBatch.DrawString(_font, $"Score: {_saved.score}", new Vector2(500, 600), Color.Black);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }


        private void Button_GoBackButton_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
    }
}
