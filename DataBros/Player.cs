using Microsoft.Xna.Framework;
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


        bool alreadyFishing = false;

        private KeyboardState oldState;
        private KeyboardState newState;

        private int pullCount = 0;

        public Vector2 p1position = GameWorld.Instance.player1.p1position;
        public Vector2 p2position = GameWorld.Instance.player2.p2position;
        public Vector2 p1AimPosition = GameWorld.Instance.player1.p1AimPosition;
        public Vector2 p2AimPosition = GameWorld.Instance.player2.p2AimPosition;




        public void Update()
        {
            oldState = newState;
            newState = Keyboard.GetState();
            //player 1 movement
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
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.RightShift))
                {
                    GameWorld.gameState.FishingKey();
                }
            }
       
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
                    GameWorld.gameState.FishingKey();
                }
            }
        }


    }
}
