using DataBros.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace DataBros.States
{
    public class GameState : State
    {
        #region Fields
        private Texture2D backgroundTexture;
        private Rectangle backgroundRectangle;

        public Song backgroundMusic;

        private List<Component> components;
        private List<Component> addComponents;
        private List<Component> delComponents;

        //Upgrade menu
        private bool playGame = false;
        private bool pickWater = true;

        private Texture2D upgradeMenuTexture;
        private Rectangle upgradeMenuRectangle;

        Texture2D buttonTexture;
        SpriteFont buttonFont;
        Button pickWaterButton;
        Button streamButton;
        Button lakeButton;
        Button oceanButton;
        public Water stream;
        string msgToPlayers = "";
        string roundOver = "";
        private int timeRemaining = 180;
        private float countDuration = 1f;
        private float currentTime = 0f;


        public Water currentWater;
        List<Fish> catchAble;

        #endregion

        #region Methods

        #region Constructor
        public GameState(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            msgToPlayers = "Press enter or space to start fishing!";
            GameWorld.repo1.Open();
            stream = GameWorld.repo1.FindWater("Stream");
            currentWater = stream;
            catchAble = GameWorld.repo1.FindAFish(currentWater.Id);

            GameWorld.repo1.Close();


            LoadContent();
            //Button
            
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
            upgradeMenuTexture = _content.Load<Texture2D>("upgrademenu");
            upgradeMenuRectangle = new Rectangle(430, 90, upgradeMenuTexture.Width, upgradeMenuTexture.Height);
        }


        #endregion
        public void LoadContent()
        {
            buttonTexture = _content.Load<Texture2D>("button");
            buttonFont = _content.Load<SpriteFont>("Fonts/font");

            // Background music
            backgroundMusic = _content.Load<Song>("Sounds/seasidewaves");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);

            spriteBatch.DrawString(buttonFont, $" When a fish has taken the bait, spam Enter/Space until pull counter is at 0", new Vector2(100, 50), Color.Green, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(buttonFont, $" {msgToPlayers}", new Vector2(200, 100), Color.Green, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(buttonFont, $"Available fish in theese waters", new Vector2(0, 200), Color.Green, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(buttonFont, $"Time remaining: {timeRemaining}", new Vector2(300, 10), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(buttonFont, $" {roundOver}", new Vector2(200, 300), Color.Red, 0f, Vector2.Zero, 8f, SpriteEffects.None, 1f);


            for (int i = 0; i < catchAble.Count; i++)
            {

                spriteBatch.DrawString(buttonFont, $" {catchAble[i].Name}", new Vector2(0, 250 + (i*50)), Color.Green, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

            }
            GameWorld.Instance.player1.Draw(spriteBatch);
            GameWorld.Instance.player2.Draw(spriteBatch);

            //Button
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            if (playGame)
            {
                spriteBatch.Draw(upgradeMenuTexture, upgradeMenuRectangle, Color.White);
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
        }
        private void StreamButton_Click(object sender, EventArgs e)
        {
            
            if (currentWater.Name != "Stream")
            {
                GameWorld.repo1.Open();
                stream = GameWorld.repo1.FindWater("Stream");
                currentWater = stream;
                msgToPlayers= $"Name {stream.Name} ";
            catchAble = GameWorld.repo1.FindAFish(currentWater.Id);

                GameWorld.repo1.Close();
                ResetWaterButtons();

            }


        }

        private void OceanButton_Click(object sender, EventArgs e)
        {
            if (currentWater.Name != "Ocean")
            {
                GameWorld.repo1.Open();
                Water ocean = GameWorld.repo1.FindWater("Ocean");
                currentWater = ocean;
                msgToPlayers = $"Name {ocean.Name} ";
                catchAble = GameWorld.repo1.FindAFish(currentWater.Id);

                GameWorld.repo1.Close();
                ResetWaterButtons();
            }

        }

        private void LakeButton_Click(object sender, EventArgs e)
        {
            if (currentWater.Name != "Lake")
            {
                GameWorld.repo1.Open();
                Water lake = GameWorld.repo1.FindWater("Lake");
                currentWater = lake;
                msgToPlayers = $"Name {lake.Name}";
                catchAble = GameWorld.repo1.FindAFish(currentWater.Id);

                GameWorld.repo1.Close();
                ResetWaterButtons();
            }
        }

        private void BaitButton_Click(object sender, EventArgs e)
        {
            GameWorld.repo1.Open();
            Bait nextBait = GameWorld.repo1.FindBait("PowerBait");
            GameWorld.repo1.Close();

            GameWorld.currentBait = nextBait;

            msgToPlayers = $"Players are now using {nextBait.BaitName}";
        }

        public void RoundOver()
        {
            roundOver = "Time's up!";
            GameWorld.currentState = GameWorld.menuState;
        }

        public override void Update(GameTime gameTime)
        {

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; 

            if (currentTime >= countDuration)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining--;
                    currentTime -= countDuration;
                }
            }
            if (timeRemaining == 0)
            {
                RoundOver();
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