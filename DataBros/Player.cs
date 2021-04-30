using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace DataBros
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get { return name; }   set { name = value; } }
        private string name = "Username";
        private string MsgToPlayer;

        public int Money { get; set; }
        public string Password { get; set; }

        private Texture2D player1Sprite;
        private Texture2D player2Sprite;
        private Texture2D p1Aim;
        private Texture2D p2Aim;
        private Texture2D fishTexture;
        private Texture2D ringTexture;
        private Texture2D bootTexture;
        private Texture2D currentTexture;


        private bool fishVisible;
        private Vector2 p2position = new Vector2(100, 900);
        private Vector2 p1position = new Vector2(700, 900);
        private Vector2 p2AimPosition = new Vector2(100, 500);
        private Vector2 p1AimPosition = new Vector2(700, 500);

        public bool isplayer1;
        public bool logedIn = false;
        public bool enablePull = false;

        int catchdifficulty;
        Fish caught;
        Color color = Color.White;
        System.Timers.Timer fishTimer;
        System.Timers.Timer catchTimer;

        private int pullPerClick;
        public bool alreadyFishing = false;

        private KeyboardState oldState;
        private KeyboardState newState;

        public int pullCount = 0;

        public SoundEffect fishEffect;
        public SoundEffect ringEffect;
        public SoundEffect bootEffect;
        public SoundEffect stepEffect;
        public SoundEffect reelEffect;
        public SoundEffect dammitEffect;
        public SoundEffect notAgainEffect;

        public void Loadcontent()
        {
            bootTexture = GameWorld.content.Load<Texture2D>("Boot");
            bootEffect = GameWorld.content.Load<SoundEffect>("Sounds/dasboot");
            ringTexture = GameWorld.content.Load<Texture2D>("Ring");
            ringEffect = GameWorld.content.Load<SoundEffect>("Sounds/MyPrecious");
            fishTexture = GameWorld.content.Load<Texture2D>("fish");
            fishEffect = GameWorld.content.Load<SoundEffect>("Sounds/fishy");

            stepEffect = GameWorld.content.Load<SoundEffect>("Sounds/Step1");
            reelEffect = GameWorld.content.Load<SoundEffect>("Sounds/fishingreel");
            dammitEffect = GameWorld.content.Load<SoundEffect>("Sounds/Damnit");
            notAgainEffect = GameWorld.content.Load<SoundEffect>("Sounds/notagain");

            if (isplayer1)
            {
                player1Sprite = GameWorld.content.Load<Texture2D>("pl1");
                p1Aim = GameWorld.content.Load<Texture2D>("p1aimsprite");

            }
            else
            {
                player2Sprite = GameWorld.content.Load<Texture2D>("pl2");
                p2Aim = GameWorld.content.Load<Texture2D>("p2aimsprite");

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isplayer1)
            {
                spriteBatch.Draw(player1Sprite, p1position, Color.White);
                spriteBatch.Draw(p1Aim, p1AimPosition, color);
                if (fishVisible)
                {
                    spriteBatch.Draw(currentTexture, new Rectangle((int)p1AimPosition.X, (int)p1AimPosition.Y, p1Aim.Width, p1Aim.Height), Color.White);
                }
                spriteBatch.DrawString(GameWorld.font, $" pulls left: {pullCount}", new Vector2(p1position.X + 90, p1position.Y + 50), Color.Green);

                spriteBatch.DrawString(GameWorld.font, $"   {Name}", new Vector2(p1position.X, p1position.Y - 25), Color.Black);
                spriteBatch.DrawString(GameWorld.font, $"Money: {Money}", new Vector2(p1position.X - 90, p1position.Y +25), Color.Black);
                if (alreadyFishing)
                {
                    spriteBatch.DrawString(GameWorld.font, $"Cant Move, busy fishing!", new Vector2(p1position.X -25, p1position.Y + 75), Color.Black);
                }
                if (MsgToPlayer != null)
                {
                    spriteBatch.DrawString(GameWorld.font, $"  {MsgToPlayer}", new Vector2(p1position.X, p1position.Y - 50), Color.Black);
                }
            }
            else
            {
                spriteBatch.Draw(player2Sprite, p2position, Color.White);
                spriteBatch.Draw(p2Aim, p2AimPosition, color);
                if (fishVisible)
                {
                    spriteBatch.Draw(currentTexture, new Rectangle((int)p2AimPosition.X, (int)p2AimPosition.Y, p2Aim.Width, p2Aim.Height), Color.White);
                }
                spriteBatch.DrawString(GameWorld.font, $"   pulls left: {pullCount}", new Vector2(p2position.X + 90, p2position.Y + 50), Color.Green);
                spriteBatch.DrawString(GameWorld.font, $"   {Name}", new Vector2(p2position.X, p2position.Y - 25), Color.Black);
                spriteBatch.DrawString(GameWorld.font, $"Money: {Money}", new Vector2(p2position.X - 90, p2position.Y + 25), Color.Black);
                if (alreadyFishing)
                {
                    spriteBatch.DrawString(GameWorld.font, $"Cant Move, busy fishing!", new Vector2(p2position.X - 25, p2position.Y + 75), Color.Black);
                }
                if (MsgToPlayer != null)
                {
                    spriteBatch.DrawString(GameWorld.font, $"  {MsgToPlayer}", new Vector2(p2position.X, p2position.Y - 50), Color.Black);
                }
            }
           
        }

        public void Update()
        {
            oldState = newState;
            newState = Keyboard.GetState();
            //player 1 movement
            if (isplayer1)
            {
                if (alreadyFishing == false)
                {
                    if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right) && p1position.X <= 700)
                    {
                        p1position.X += 100;
                        p1AimPosition.X += 100;
                        stepEffect.Play();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left) && p1position.X >= 100)
                    {
                        p1position.X -= 100;
                        p1AimPosition.X -= 100;
                        stepEffect.Play();
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up) && p1AimPosition.Y >= 100)
                    {
                        p1AimPosition.Y -= 100;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down) && p1AimPosition.Y <= 600)
                    {
                        p1AimPosition.Y += 100;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    {
                        FishingKey();
                        reelEffect.Play();
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    {
                        if (pullCount > 0)
                        {
                            pullCount -= 1;
                        }
                        if (enablePull)
                        {
                            if (p1AimPosition.Y < 800)
                            {
                                p1AimPosition.Y += pullPerClick;
                            }
                        }
                    }
                }
                // shallow, middle and deep water difficulty modifiers, changes acording to where player is chosing to fish.
                // Is added to chance to catch a fish. easiest to catch a fish in shallow water and hardest in deep water
                if (p1AimPosition.Y > 600)
                {
                    catchdifficulty = 1;
                }
                else if (p1AimPosition.Y <= 600 & p1AimPosition.Y > 300)
                {
                    catchdifficulty = 2;

                }
                else
                {
                    catchdifficulty = 3;

                }
            }
            else
            {
                if (alreadyFishing == false)
                {
                    //player 2 movement
                    if (newState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D) && p2position.X <= 700)
                    {
                        p2position.X += 100;
                        p2AimPosition.X += 100;
                        stepEffect.Play();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A) && p2position.X >= 100)
                    {
                        p2position.X -= 100;
                        p2AimPosition.X -= 100;
                        stepEffect.Play();
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W) && p2AimPosition.Y >= 100)
                    {
                        p2AimPosition.Y -= 100;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S) && p2AimPosition.Y <= 600)
                    {
                        p2AimPosition.Y += 100;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                    {
                     
                        FishingKey();
                        reelEffect.Play();

                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                    {
                        if (pullCount > 0)
                        {
                            pullCount -= 1;
                        }
                        if (enablePull)
                        {
                            if (p2AimPosition.Y < 800)
                            {
                                p2AimPosition.Y += pullPerClick;
                            }
                        }
                    }
                }
            }
            // shallow, middle and deep water difficulty modifiers, changes acording to where player is chosing to fish.
            // Is added to chance to catch a fish. easiest to catch a fish in shallow water and hardest in deep water
            if (p2AimPosition.Y > 600)
            {
                catchdifficulty = 1;
            }
            else if (p2AimPosition.Y <= 600 & p2AimPosition.Y > 300)
            {
                catchdifficulty = 2;

            }
            else
            {
                catchdifficulty = 3;

            }
        }

        public void FishingKey()
        {

            pullCount = 0;
            fishTimer = new System.Timers.Timer();
                fishTimer.Elapsed += new ElapsedEventHandler(OnTimedEventFishing);
                if (GameWorld.currentBait.Alive)
                {
                    fishTimer.Interval = 3000 * GameWorld.currentBait.BiteTime *0.5f;
                }
                else
                {
                    fishTimer.Interval = 3000 * GameWorld.currentBait.BiteTime;
                }
                fishTimer.Enabled = true;
                alreadyFishing = true;
                MsgToPlayer = "";
            
        }

        private void OnTimedEventFishing(object sender, ElapsedEventArgs e)
        {
            Random Rndchance = new Random();
            int chanceToCatch = Rndchance.Next( 1, 10);
            chanceToCatch += catchdifficulty;

            Debug.WriteLine($"{chanceToCatch}");
            Debug.WriteLine($"difficulty{catchdifficulty} position{p2AimPosition.Y}");

            if (chanceToCatch > 6)
            {
                MsgToPlayer = $"A fish has taken the bait! time to wheel it in (Press enter / space)!!";
                color = Color.Black;
                Random rnd = new Random();
                pullCount = rnd.Next(10, 20);
                List<Fish> catchAble;
                if (isplayer1)
                {
                    GameWorld.repo1.Open();
                    catchAble = GameWorld.repo1.FindAFish(GameWorld.gameState.currentWater.Id);
                    GameWorld.repo1.Close();
                }
                else
                {
                    GameWorld.repo2.Open();
                    catchAble = GameWorld.repo2.FindAFish(GameWorld.gameState.currentWater.Id);
                    GameWorld.repo2.Close();
                }
              

                int max = catchAble.Count;
                    Random Rnd = new Random();
                    int randomNumber = Rnd.Next(0, max);

                    caught = catchAble[randomNumber];
                pullCount += caught.Strenght;
                CalcPullPerClick();

                Debug.WriteLine(pullPerClick);
                if (caught.Name == "Boot")
                {
                    currentTexture = bootTexture;
                    bootEffect.Play();
                }
                else if (caught.Name == "The one ring to rule them all")
                {
                    currentTexture = ringTexture;
                    ringEffect.Play();
                }
                else
                {
                    currentTexture = fishTexture;
                    fishEffect.Play();
                }
                fishVisible = true;

                catchTimer = new System.Timers.Timer();
                catchTimer.Elapsed += new ElapsedEventHandler(OnTimedEventCatching);
                catchTimer.Interval = 5000;
                catchTimer.Enabled = true;
                enablePull = true;
            }
            else
            {
                MsgToPlayer = "You didnt catch a thing!!";
                alreadyFishing = false;
                dammitEffect.Play();
            }

            fishTimer.Elapsed -= new ElapsedEventHandler(OnTimedEventFishing);
            fishTimer.Enabled = false;
        }

        private void OnTimedEventCatching(object sender, ElapsedEventArgs e)
        {
            fishVisible = false;
            enablePull = false;
            if (pullCount == 0)
            {
                if (isplayer1)
                {
                    GameWorld.repo1.Open();
                    MsgToPlayer = $"You have caught a {caught.Name} at weight {caught.Weight}Kg going for {caught.Price}Monies !!";
                    Money += caught.Price;
                    GameWorld.repo1.UpdatePlayers(name, Money);
                    GameWorld.repo1.Close();
                }
                else
                {
                    GameWorld.repo2.Open();
                    MsgToPlayer = $"You have caught a {caught.Name} at weight {caught.Weight}Kg going for {caught.Price}Monies !!";
                    Money += caught.Price;
                    GameWorld.repo2.UpdatePlayers(name, Money);
                    GameWorld.repo2.Close();
                }
            
            }
            else
            {
                MsgToPlayer= $"Fish slipped away ";
                notAgainEffect.Play();
            }
            color = Color.White;
            catchTimer.Elapsed -= new ElapsedEventHandler(OnTimedEventCatching);
            catchTimer.Enabled = false;
            alreadyFishing = false;

            ResetAim();

        }
        /// <summary>
        /// Calculates how much the fish shall be pulled each time the player pulls
        /// </summary>
        private void CalcPullPerClick()
        {
            if (isplayer1)
            {
                float startPos = p1AimPosition.Y;
                float endPos = p1position.Y - 100;
                pullPerClick = (int)(((endPos - startPos)) / pullCount);

            }

            else
            {
                float startPos = p2AimPosition.Y;
                float endPos = p2position.Y - 100;
                pullPerClick = (int)(((endPos - startPos)) / pullCount);

            }
        }
        /// <summary>
        /// //  Resets the aim after a player tried to fish
        /// </summary>
        private void ResetAim()
        {
            if (isplayer1)
            {
                p1AimPosition.Y = 700;
            }
            else
            {
                p2AimPosition.Y = 700;
            }
        }

    }
}
