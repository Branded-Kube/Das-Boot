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
        private bool isCreatingUser = false;
        public string menyMsg = "";
        public bool inMenu = false;

        public bool IsCreatingUser {
            get
            {
                return isCreatingUser;
            }
                 set
            {
                isCreatingUser = value;
            }
                
                }
        #endregion

        #region Methods

        #region Constructor
        public MenuState(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            //Button
            var buttonTexture = _content.Load<Texture2D>("button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/font");
            menyMsg = "Create 2 users, and login with both.";
            var startGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(495, 250),
                Text = "Start Game",
            };

            startGameButton.Click += NewGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(495, 305),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            var CreateUserButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(495,380),
                Text = "Create new user",

            };

            CreateUserButton.Click += CreateNewUserButton_Click;
            
            var DeleteUserButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 380),
                Text = "Delete All users",

            };

            DeleteUserButton.Click += DeleteUserButton_Click;

            var UserLoginButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(495, 430),
                Text = "Login",

            };

            UserLoginButton.Click += UserLoginButton_Click;


            components = new List<Component>()
            {
                DeleteUserButton,
                startGameButton,
                quitGameButton,
                CreateUserButton,
                UserLoginButton,
            };
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            GameWorld.repo.Open();
           GameWorld.repo.DelPlayers();
            GameWorld.repo.Close();

        }

        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            if (isCreatingUser)
            {
                spriteBatch.DrawString(GameWorld.font, "Enter your username", new Vector2((GameWorld._graphics.PreferredBackBufferWidth / 2) - 100, 700), Color.Black, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(GameWorld.font, UserLogin.PlayerNameInput, new Vector2((GameWorld._graphics.PreferredBackBufferWidth / 2) - 100, 750), Color.Green, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(GameWorld.font, "Enter your password", new Vector2((GameWorld._graphics.PreferredBackBufferWidth / 2) - 100, 800), Color.Black, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(GameWorld.font, UserLogin.PasswordInputString, new Vector2((GameWorld._graphics.PreferredBackBufferWidth / 2) - 100, 850), Color.Green, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            }
                spriteBatch.DrawString(GameWorld.font, $"Player 2: {GameWorld.Instance.player2.Name}", new Vector2((GameWorld._graphics.PreferredBackBufferWidth / 2) + 150, 900), Color.Black, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(GameWorld.font, $"Player 1: {GameWorld.Instance.player1.Name}", new Vector2((GameWorld._graphics.PreferredBackBufferWidth / 2) - 400, 900), Color.Black, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(GameWorld.font, $"{menyMsg}", new Vector2((GameWorld._graphics.PreferredBackBufferWidth / 2) -400, 200), Color.Black, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);


            
            spriteBatch.End();
        }

        //Start Game & Quit Game buttons
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            if (GameWorld.Instance.player2.logedIn == true & GameWorld.Instance.player1.logedIn == true)
            {
                GameWorld.gameState = new GameState(_game, _graphicsDevice, _content);

                _game.ChangeState(GameWorld.gameState);
            }
            else
            {
                menyMsg = "Please login with 2 difrent users to start";
            }

        }

        private void CreateNewUserButton_Click(object sender, EventArgs e)
        {
            menyMsg = "Write your name and Press enter to confirm";

            if ( inMenu == false)
            {
                GameWorld.Instance.AddCreateUserLogin();
                inMenu = true;
            }
            if (isCreatingUser == false)
            {
                isCreatingUser = true;
            }

        }

        private void UserLoginButton_Click(object sender, EventArgs e)
        {
            menyMsg = "Write your username and press enter ";
            if (inMenu == false)
            {
                GameWorld.Instance.AddUserLogin();
                inMenu = true;
            }
            if (isCreatingUser == false)
            {
                isCreatingUser = true;
            }
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

        //public override void Load(ContentManager content)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}

