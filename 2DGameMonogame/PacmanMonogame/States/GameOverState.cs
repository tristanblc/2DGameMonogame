﻿using Microsoft.Xna.Framework;
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
    public class GameOverState : State
    {
        private List<Button> components;
        private SpriteFont _font;
        private Texture2D _texture;
        public GameOverState(Jeu game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            _texture = _content.Load<Texture2D>("gameover");
            _font = _content.Load<SpriteFont>("Font");

            var newGameButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(500, 400),
                Text = "New Game",
            };

            newGameButton.Click += Button_NewGame_Click;

            var menuGameButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(900, 400),
                Text = "Go to Menu",
            };


            menuGameButton.Click += Button_Menu_Clicked;

            var exitGameButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(1300, 400),
                Text = "Exit",
            };


            exitGameButton.Click += Button_Exit_Clicked;

            components = new List<Button>()
            {
                newGameButton,
                menuGameButton,
                exitGameButton
            };
        }

        public override void LoadContent()
        {

        }

        private void Button_NewGame_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_Menu_Clicked(object sender, EventArgs args)
        {
           _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
        private void Button_Exit_Clicked(object sender, EventArgs args)
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
 
            spriteBatch.Draw(_texture, new Vector2(650, 100), Color.White);



            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
