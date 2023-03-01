using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PäcmanMonogame.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.States
{
    public class PauseMenuState : State
    {
        private Jeu _game;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _contentManager;
        private List<Button> _components;
        private SpriteFont _font;

        public PauseMenuState(Jeu game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _contentManager = content;

            var buttonTexture = _content.Load<Texture2D>("Button");
            _font = _content.Load<SpriteFont>("Font");



            var exitButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(500, 200),
                Text = "",
                Name = "exitButton"
            };
            exitButton.Click += Button_ExitButton_Click;


            var backButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(500, 400),
                Text = "",
                Name = "backButton"
            };
            backButton.Click += Button_BackButton_Click;

            _components = new List<Button>()
            {
               exitButton,
               backButton

            };
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

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void Button_ExitButton_Click(object sender, EventArgs args)
        {
            _game.Exit();
        }
        private void Button_BackButton_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
    }
}
