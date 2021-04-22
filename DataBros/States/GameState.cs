using DataBros.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataBros.States
{
    public class GameState : State
    {
        #region Fields
        private Texture2D backgroundTexture;
        private Rectangle backgroundRectangle;

        private List<Component> components;
        private List<Component> addComponents;
        private List<Component> delComponents;




        //Upgrade menu
        private bool paused = false;
        private bool pickWater = true;



        private Texture2D upgradeMenuTexture;
        private Rectangle upgradeMenuRectangle;

        private Texture2D upgrade1;
        private Texture2D upgrade2;
        private Rectangle upgRectangle;
        private Rectangle upg2Rectangle;
        Texture2D buttonTexture;
        SpriteFont buttonFont;
        Button pickWaterButton;
        Button streamButton;
        Button lakeButton;
        Button oceanButton;
        int currentWatersize;
        #endregion

        #region Methods

        #region Constructor
        public GameState(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            
            //Button
            buttonTexture = _content.Load<Texture2D>("button");
            buttonFont = _content.Load<SpriteFont>("Fonts/font");
            pickWaterButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(5, 10),
                Text = "Pick Water",
            };

            pickWaterButton.Click += PickWaterButton_Click;

            var baitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(5, 65),
                Text = "Bait",
            };

            baitButton.Click += BaitButton_Click;

            lakeButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 100),
                Text = "Lake",
            };


            oceanButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Ocean",
            };


            streamButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Stream",
            };

            components = new List<Component>()
            {
                pickWaterButton,
                baitButton,
            };

            //Background
            backgroundTexture = _content.Load<Texture2D>("background1");
            backgroundRectangle = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);

            //Upgrade menu
            upgradeMenuTexture = _content.Load<Texture2D>("upgmenu");
            upgradeMenuRectangle = new Rectangle(430, 90, upgradeMenuTexture.Width, upgradeMenuTexture.Height);

            upgrade1 = _content.Load<Texture2D>("upgrade1");
            upgrade2 = _content.Load<Texture2D>("upgrade2");
            upgRectangle = new Rectangle(495, 140, upgrade1.Width, upgrade1.Height);
            upg2Rectangle = new Rectangle(495, 195, upgrade2.Width, upgrade2.Height);

        }


        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);

            GameWorld.visualManager.Draw(spriteBatch);

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

        private void PickWaterButton_Click(object sender, EventArgs e)
        {
            if (pickWater == true)
            {
                lakeButton.Click += LakeButton_Click;
                oceanButton.Click += OceanButton_Click;
                streamButton.Click += StreamButton_Click;

                addComponents = new List<Component>()
            {
                streamButton,
                lakeButton,
                oceanButton,
            };
            }
            pickWater = false;


        }
        public void ResetWaterButtons()
        {
            delComponents = new List<Component>()
            {
                streamButton,
                lakeButton,
                  oceanButton,
            };
            lakeButton.Click -= LakeButton_Click;
            oceanButton.Click -= OceanButton_Click;
            streamButton.Click -= StreamButton_Click;
            pickWater = true;
            GameWorld.visualManager.cellCount = currentWatersize;
            GameWorld.visualManager.CreateGrid();

        }
        private void StreamButton_Click(object sender, EventArgs e)
        {
            GameWorld.repo.Open();
            Water stream = GameWorld.repo.FindWater("Stream");
            Debug.WriteLine($"Id {stream.Id} Name {stream.Name} Size {stream.Size} Type {stream.Type} ");
            GameWorld.repo.Close();
            currentWatersize = stream.Size;
            ResetWaterButtons();

        }

        private void OceanButton_Click(object sender, EventArgs e)
        {
            GameWorld.repo.Open();
            Water ocean = GameWorld.repo.FindWater("Ocean");
            Debug.WriteLine($"Id {ocean.Id} Name {ocean.Name} Size {ocean.Size} Type {ocean.Type} ");
            GameWorld.repo.Close();
            currentWatersize = ocean.Size;
            ResetWaterButtons();

        }

        private void LakeButton_Click(object sender, EventArgs e)
        {
            GameWorld.repo.Open();
            Water lake = GameWorld.repo.FindWater("Lake");
            Debug.WriteLine($"Id {lake.Id} Name {lake.Name} Size {lake.Size} Type {lake.Type} ");
            GameWorld.repo.Close();
            currentWatersize = lake.Size;
            ResetWaterButtons();

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

            foreach (Cell cell in GameWorld.visualManager.grid)
            {
                cell.MyColor = Color.Yellow;
            }

            if (addComponents != null)
            {
                components.AddRange(addComponents);
                addComponents.Clear();
            }
            if (delComponents != null)
            {
                foreach (Component component in delComponents)
                {
                    components.Remove(component);
                }
                delComponents.Clear();
            }

            //Button
            foreach (var component in components)
            {
                component.Update(gameTime);
            }



        }

        #endregion
    }
}