using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PäcmanMonogame.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;

namespace PacmanMonogame.States
{
    public class MenuState : State
    {
        private List<Button> components;
        private SpriteFont _font;
        public MenuState(Jeu game, GraphicsDevice graphicsDevice, ContentManager content)
           : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            _font = _content.Load<SpriteFont>("Font");

            var newGameButton = new Button(buttonTexture,_font)
            {
                Position = new Vector2(500, 400),
                Text = "New Game",
            };

            newGameButton.Click += Button_NewGame_Click;

            var keyboardMenuButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(900, 400),
                Text = "Change controls",
            };

            keyboardMenuButton.Click += Button_KeyboardMenuButton_Click;



            var quitGameButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(1300, 400),
                Text = "Quit Game",
            };

            quitGameButton.Click += Button_Quit_Clicked;

            components = new List<Button>()
            {
                newGameButton,
                keyboardMenuButton,
                quitGameButton,
            };
        }

        public override void LoadContent()
        {
           
        }

        private void Button_NewGame_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_KeyboardMenuButton_Click(object sender,EventArgs e)
        {
            _game.ChangeState(new KeyBoardMenuState(_game, _graphicsDevice, _content));
        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
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
            spriteBatch.DrawString(_font, "Shooter game", new Vector2(850, 200), Color.Black);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
