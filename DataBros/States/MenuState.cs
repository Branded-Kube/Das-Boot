using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DataBros.Controls;

namespace DataBros.States
{
    public class MenuState : State
    {
        #region Fields
        private List<Component> components;
        #endregion

        #region Methods

        #region Constructor
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            //Button
            var buttonTexture = _content.Load<Texture2D>("button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/font");

            var startGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(350, 150),
                Text = "Start Game",
            };

            startGameButton.Click += NewGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(350, 200),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<Component>()
            {
                startGameButton,
                quitGameButton,
            };
        }

        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        //Start Game & Quit Game buttons
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //remove sprites if they are not needed
        }

        public override void Update(GameTime gameTime)
        {
            //Button
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
        }

        public override void Load(ContentManager content)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

