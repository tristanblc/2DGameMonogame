using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PäcmanMonogame.Controls;
using PacmanMonogame.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.States
{
    public class KeyBoardMenuState : State
    {
        private List<Button> components;
        private SpriteFont _font;
        private KeyboardState currentKey;
        private Button currentButton;
        private IService service;
        public KeyBoardMenuState(Jeu game, GraphicsDevice graphicsDevice, ContentManager content)
           : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            _font = _content.Load<SpriteFont>("Font");




            var SwitchButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(500, 200),
                Text = "",
                Name = "SwitchButton"
            };
            SwitchButton.Click += Button_SwitchButton_Click;


            var SpecialButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(1000, 200),
                Text = "",
                Name = "SpecialButton"
            };
            SpecialButton.Click += Button_Special_Click;

            var UpKeyButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(500, 400),
                Text = "",
                Name = "UpKeyButton"
            };

            UpKeyButton.Click += Button_UpKey_Click;

            var DownKeyButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(500, 700),
                Text = "",
                Name = "DownKeyButton"
            };

            DownKeyButton.Click += Button_DownKey_Clicked;


            var LeftKeyButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(1000, 400),
                Text = "",
                Name = "LeftKeyButton"
            };

            LeftKeyButton.Click += Button_LeftKey_Click;

            var RightKeyButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(1000,700),
                Text = "",
                Name = "RightKeyButton"
            };
        

            RightKeyButton.Click += Button_RightKey_Click;



            var backButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(1500, 700),
                Text = "Back to menu",
                Name = "BackButton"
            };


            backButton.Click += Button_backButton_Click;

            var attackButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(1500, 400),
                Text = "",
                Name = "AttackButton"
            };


            attackButton.Click += Button_attackButton_Click;


            var saveButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(1800, 700),
                Text = "Save",
            };
            saveButton.Click += Button_saveButton_Click;
         

            components = new List<Button>()
            {
                UpKeyButton,
                RightKeyButton,
                LeftKeyButton,
                DownKeyButton,
                backButton,
                SwitchButton,
                SpecialButton,
                attackButton,
                saveButton,

            };
          
            currentKey = Keyboard.GetState();
            service = new Service();
            components = service.ReadSavedKeysMenu(components);

        }

        public override void LoadContent()
        {

        }

        private void Button_UpKey_Click(object sender, EventArgs e)
        {
            currentButton = components[0]; 
        }

        private void Button_DownKey_Clicked(object sender, EventArgs args)
        {
            currentButton = components[3];
           
        }

        private void Button_LeftKey_Click(object sender, EventArgs e)
        {
            currentButton = components[2];
        }

        private void Button_RightKey_Click(object sender, EventArgs args)
        {

            currentButton = components[1];
        }

        private void Button_Special_Click(object sender, EventArgs args)
        {
            currentButton = components[6];
        }

        private void Button_SwitchButton_Click(object sender,EventArgs args)
        {
            currentButton = components[5];
        }
        private void Button_attackButton_Click(object sender, EventArgs args)
        {
            currentButton = components[7];
        }

        private void Button_backButton_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void Button_saveButton_Click(object sender, EventArgs args)
        {
            service.SaveKeyInJson(components);
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);


            foreach(var component in components)
            {
               if(component == currentButton)
                {
                    var keyboardState = Keyboard.GetState();
                    var keys = keyboardState.GetPressedKeys();

                    if (keys.Length > 0)
                    {
                        var keyValue = keys[0].ToString();

                        
                        component.Text = "";
                        component.Text += keyValue;
                    }
                    component.Update(gameTime);
      
                }

            }

            
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();


            spriteBatch.DrawString(_font, "Set switch key :",new Vector2(200, 200), Color.Black);
            spriteBatch.DrawString(_font, "Set Special Key :", new Vector2(800, 200), Color.Black);
            spriteBatch.DrawString(_font, "Set Up Key :", new Vector2(200, 400),Color.Black);
            spriteBatch.DrawString(_font,"Set Down Key :", new Vector2(200,700),Color.Black);
            spriteBatch.DrawString(_font, "Set Left Key : ", new Vector2(800, 400), Color.Black);
            spriteBatch.DrawString(_font,"Set  Right Key: ", new Vector2(800,700),Color.Black);
            spriteBatch.DrawString(_font, "Set  Attack Key: ", new Vector2(1300, 400), Color.Black);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

      

            spriteBatch.End();
        }
    }
}
