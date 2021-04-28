using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace DataBros
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get { return name; }   set { name = value; } }
        private string name = "Username";

        //public string Name { get; set; }

        public int Money { get; set; }
        public string Password { get; set; }

        private Texture2D player1Sprite;
        private Texture2D player2Sprite;
        private Texture2D p1Aim;
        private Texture2D p2Aim;
        private Vector2 p1origin;
        private Vector2 p2origin;
        private Vector2 p1position = new Vector2(700, 900);
        private Vector2 p2position = new Vector2(100, 900);
        private Vector2 p1AimPosition = new Vector2(700, 500);
        private Vector2 p2AimPosition = new Vector2(100, 500);
        private bool isplayer1;

        bool alreadyFishing = false;

        private KeyboardState oldState;
        private KeyboardState newState;

        private int pullCount = 0;


        public Player(bool isplayer1)
        {
            this.isplayer1 = isplayer1;
        }

        public Player()
        {


        }

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
                spriteBatch.Draw(p1Aim, p1AimPosition, Color.White);

            }
            else
            {
                spriteBatch.Draw(player2Sprite, p2position, Color.White);
                spriteBatch.Draw(p2Aim, p2AimPosition, Color.White);
            }
            spriteBatch.DrawString(GameWorld.font, $"ammount of pulls left: {pullCount}", new Vector2(10, 300), Color.Green);
        }

        public void Update()
        {
            oldState = newState;
            newState = Keyboard.GetState();
            //player 1 movement
            if (alreadyFishing == false)
            {
                if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right) && p1position.X <= 700)
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
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.RightShift))
                {
                    GameWorld.gameState.FishingKey();
                }
            }
       
            if (alreadyFishing == false)
            {
                //player 2 movement
                if (newState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D) && p2position.X <= 700)
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
                    GameWorld.gameState.FishingKey();
                }
            }
        }


    }
}
