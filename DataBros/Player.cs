using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace DataBros
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get { return name; }   set { name = value; } }
        private string name = "Username";
        private string MsgToPlayer;


        //public string Name { get; set; }

        public int Money { get; set; }
        public string Password { get; set; }

        private Texture2D player1Sprite;
        private Texture2D player2Sprite;
        private Texture2D p1Aim;
        private Texture2D p2Aim;
        private Vector2 p1origin;
        private Vector2 p2origin;
        private Vector2 p2position = new Vector2(700, 900);
        private Vector2 p1position = new Vector2(100, 900);
        private Vector2 p2AimPosition = new Vector2(700, 500);
        private Vector2 p1AimPosition = new Vector2(100, 500);
        public bool isplayer1;
        public bool logedIn = false;
        int catchdifficulty;
        Fish caught;
        Color color = Color.White;
        System.Timers.Timer fishTimer;
        System.Timers.Timer catchTimer;


        public bool alreadyFishing = false;

        private KeyboardState oldState;
        private KeyboardState newState;

        public int pullCount = 0;

        public void Loadcontent()
        {
            if (isplayer1)
            {
                player1Sprite = GameWorld.content.Load<Texture2D>("pl1");
                p1origin = new Vector2(500, 300);
                p1Aim = GameWorld.content.Load<Texture2D>("p1aimsprite");
            }
            else
            {
                player2Sprite = GameWorld.content.Load<Texture2D>("pl2");
                p2origin = new Vector2(300, 300);
                p2Aim = GameWorld.content.Load<Texture2D>("p2aimsprite");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isplayer1)
            {
                spriteBatch.Draw(player1Sprite, p1position, Color.White);
                spriteBatch.Draw(p1Aim, p1AimPosition, color);
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
                    if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right) && p1position.X <= 900)
                    {
                        p1position.X += 100;
                        p1AimPosition.X += 100;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left) && p1position.X >= 100)
                    {
                        p1position.X -= 100;
                        p1AimPosition.X -= 100;
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
                    }
                }
            }
            else
            {
                if (alreadyFishing == false)
                {
                    //player 2 movement
                    if (newState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D) && p2position.X <= 900)
                    {
                        p2position.X += 100;
                        p2AimPosition.X += 100;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A) && p2position.X >= 100)
                    {
                        p2position.X -= 100;
                        p2AimPosition.X -= 100;
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
                    }
                }
            }

            if (p1AimPosition.Y > 600)
            {
                catchdifficulty = 3;
            }
            else if (p1AimPosition.Y <= 600 & p1AimPosition.Y > 300)
            {
                catchdifficulty = 2;

            }
            else
            {
                catchdifficulty = 1;

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
            // catchdifficulty
            Random Rndchance = new Random();
            int chanceToCatch = Rndchance.Next( 1, 10);
            chanceToCatch += catchdifficulty;
            Debug.WriteLine($"{chanceToCatch}");
            Debug.WriteLine($"difficulty{catchdifficulty} position{p1AimPosition.Y}");


            if (chanceToCatch > 6)
            {
                MsgToPlayer = $"A fish has taken the bait! time to wheel it in (Press enter / space)!!";
                color = Color.Black;
                Random rnd = new Random();
                pullCount = rnd.Next(10, 20);

                GameWorld.repo.Open();
                    var catchAble = GameWorld.repo.FindAFish(GameWorld.gameState.currentWater.Id);
                GameWorld.repo.Close();

                int max = catchAble.Count;
                    Random Rnd = new Random();
                    int randomNumber = Rnd.Next(0, max);

                    caught = catchAble[randomNumber];
                pullCount += caught.Strenght;

                catchTimer = new System.Timers.Timer();
                catchTimer.Elapsed += new ElapsedEventHandler(OnTimedEventCatching);
                catchTimer.Interval = 5000;
                catchTimer.Enabled = true;


            }
            else
            {
                MsgToPlayer = "You didnt catch a thing!!";
                alreadyFishing = false;
            }

            fishTimer.Elapsed -= new ElapsedEventHandler(OnTimedEventFishing);
            fishTimer.Enabled = false;

        }

        private void OnTimedEventCatching(object sender, ElapsedEventArgs e)
        {
            if (pullCount == 0)
            {
                GameWorld.repo.Open();

                MsgToPlayer = $"You have caught a {caught.Name} at weight {caught.Weight}Kg going for {caught.Price}Monies !!";
                Money += caught.Price;
                GameWorld.repo.UpdatePlayers(name, Money);
                GameWorld.repo.Close();
            }
            else
            {
                MsgToPlayer= $"Fish slipped away ";
            }
            color = Color.White;
            catchTimer.Elapsed -= new ElapsedEventHandler(OnTimedEventCatching);
            catchTimer.Enabled = false;
            alreadyFishing = false;

        }
    }
}
