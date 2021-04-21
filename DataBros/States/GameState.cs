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

        //Upgrade menu
        private bool paused = false;

        private Texture2D upgradeMenuTexture;
        private Rectangle upgradeMenuRectangle;

        private Texture2D upgrade1;
        private Texture2D upgrade2;
        private Rectangle upgRectangle;
        private Rectangle upg2Rectangle;
        #endregion

        #region Methods

        #region Constructor
        public GameState(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            //Button
            var buttonTexture = _content.Load<Texture2D>("button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/font");

            var fishingPoleButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(5, 5),
                Text = "Fishing Pole",
            };

            fishingPoleButton.Click += FishingPoleButton_Click;

            var baitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(5, 55),
                Text = "Bait",
            };

            baitButton.Click += BaitButton_Click;

            components = new List<Component>()
            {
                fishingPoleButton,
                baitButton,
            };

            //Upgrade menu
            upgradeMenuTexture = _content.Load<Texture2D>("upgmenu");
            upgradeMenuRectangle = new Rectangle(275, 40, upgradeMenuTexture.Width, upgradeMenuTexture.Height);

            upgrade1 = _content.Load<Texture2D>("upgrade1");
            upgrade2 = _content.Load<Texture2D>("upgrade2");
            upgRectangle = new Rectangle(335, 90, upgrade1.Width, upgrade1.Height);
            upg2Rectangle = new Rectangle(335, 145, upgrade2.Width, upgrade2.Height);
        }

        #endregion

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
                spriteBatch.Draw(upgradeMenuTexture, upgradeMenuRectangle, Color.White);
                spriteBatch.Draw(upgrade1, upgRectangle, Color.White);
                spriteBatch.Draw(upgrade2, upg2Rectangle, Color.White);

                if (Keyboard.GetState().IsKeyDown(Keys.U))
                {
                    //Upgrade fishing pole or bait
                }
            }
            spriteBatch.End();
        }

        private void FishingPoleButton_Click(object sender, EventArgs e)
        {
            paused = !paused;
        }

        private void BaitButton_Click(object sender, EventArgs e)
        {
            paused = !paused;
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

        #endregion
    }
}