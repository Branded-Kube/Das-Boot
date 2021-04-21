using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBros.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DataBros.States
{
    public class GameState : State
    {
        #region Fields
        private List<Component> components;

        private bool paused = false;
        private bool isClicked = false;
        private Texture2D menuTexture;
        private Rectangle menuRectangle;

        private Button fishingPoleBtn, baitBtn;

        private Texture2D upgradeMenuTexture;
        private Rectangle upgradeRectangle;
        #endregion

        #region Methods

        #region Constructor
        public GameState(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            //Button
            var buttonTexture = _content.Load<Texture2D>("button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/font");

            upgradeMenuTexture = _content.Load<Texture2D>("upgrademenu");
            upgradeRectangle = new Rectangle(100,10, upgradeMenuTexture.Width, upgradeMenuTexture.Height);

            var fishingPoleButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(5, 5),
                Text = "Fishing Pole",
            };

            fishingPoleButton.Click += BaitButton_Click;

            var baitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(5, 50),
                Text = "Bait",
            };

            baitButton.Click += FishingPoleButton_Click;

            components = new List<Component>()
            {
                fishingPoleButton,
                baitButton,
            };
        }

        #endregion

        public override void Load(ContentManager content)
        {
            menuTexture = content.Load<Texture2D>("upgrademenu");
            menuRectangle = new Rectangle(0, 0, menuTexture.Width, menuTexture.Height);


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //Button
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            if (paused)
            {
                //spriteBatch.Draw(menuTexture, menuRectangle, Color.White);
                spriteBatch.Draw(upgradeMenuTexture, upgradeRectangle, Color.White);

            }
            spriteBatch.End();
        }

        private void FishingPoleButton_Click(object sender, EventArgs e)
        {
            paused = !paused;
        }

        private void BaitButton_Click(object sender, EventArgs e)
        {

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

            MouseState mouse = Mouse.GetState();

            if (!paused)
            {
                //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                //{
                //    paused = true;
                //}
            }

        }

        #endregion
    }
}