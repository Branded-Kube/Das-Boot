using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using DataBros.States;
using Microsoft.Xna.Framework.Content;
using System;

namespace DataBros
{
    public class GameWorld : Game
    {
        private static GameWorld instance;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }


        public static GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        public static SpriteFont font;
        public static Texture2D player1Sprite;
        public static Texture2D player2Sprite;
        public Player player1;
        public Player player2;
        public static Bait currentBait;
        
        //states
        public static State currentState;
        public static MenuState menuState;
        public static GameState gameState;


        private static State nextState;
        //UserLogin userLogin;

        

        public void ChangeState(State state)
        {
            nextState = state;
        }


        // Custom classes/objects
       // public static VisualManager visualManager;
        public static ContentManager content;

        // Ints / points
        //private int sizeX = 900;
        //private int sizeY = 1100;

        public static Repository repo;
        public static Repository repo2;




        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;

            player1 = new Player();
            player2 = new Player();

            // Database
            var mapper = new Mapper();
            var provider = new SQLiteDatabaseProvider("Data Source=adventurer.db;Version=3;new=true");

            repo = new Repository(provider, mapper);

            repo.Open();

            repo.AddWater("Lake", 20,true);
            repo.AddWater("Ocean", 100, false);
            repo.AddWater("Stream", 10, true);

            repo1.AddFish("Herring", 5, 1, 1, 1);
            repo1.AddFish("Cod", 35, 20, 1, 4);
            repo1.AddFish("Boot", 50, 2, 1, 30);
            repo1.AddFish("Tuna", 140, 80, 3, 2);
            repo1.AddFish("Catfish", 5, 1, 1, 1);

            repo1.AddFish("Flatfish", 50, 25, 2, 3);
            repo1.AddFish("Tigershark", 70, 50,2, 4);
            repo1.AddFish("Squid", 40, 20, 2, 4);
            repo1.AddFish("Boot", 50, 2, 2, 30);

            repo1.AddFish("Bass", 50, 1, 1, 1);
            repo1.AddFish("The one ring to rule them all", 1000, 500, 3, 35);
            repo1.AddFish("Salmon", 60, 60, 3, 3);


            repo1.AddBait("Earthworm", 5, 3, true);
            repo1.AddBait("PowerBait", 10, 1, false);
            repo1.AddBait("Herring", 20, 2, false);


            currentBait = repo.FindBait("Regnorm");

            repo.Close();

            //var mapper1 = new Mapper();
            //var provider1 = new SQLiteDatabaseProvider("Data Source=adventurer.db;Version=3;new=true");

            //List<Bait> result1;
            //var repo1 = new Repository(provider1, mapper1);

            //repo.Open();

          
            //result1 = repo.GetAllBait();
            //foreach (var bait in result1)
            //{
            //    Debug.WriteLine($"Id {bait.Id} Name {bait.BaitName} Cost {bait.Price}");

            //}
            //var anotherBait = repo.FindBait("Regnorm");
            //Debug.WriteLine($"Id {anotherBait.Id} Name {anotherBait.BaitName} Cost {anotherBait.Price}");

            // repo.Close();
        }

        protected override void Initialize()
        {

            // Sets window size
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 1000;

            //visualManager = new VisualManager(_spriteBatch, new Rectangle(-566, 0, sizeX, sizeY));

            _graphics.ApplyChanges();
            base.Initialize();

        }


        public void AddUserLogin()
        {
            Window.TextInput += UserLogin.UsernameInput;

            Window.TextInput += UserLogin.PasswordInput;
        }
        public void RemoveUserLogin()
        {
            Window.TextInput -= UserLogin.UsernameInput;

            Window.TextInput -= UserLogin.PasswordInput;
            menuState.inMenu = false;
        }

        public void AddCreateUserLogin()
        {
            Window.TextInput += UserLogin.CreateUsernameInput;

            Window.TextInput += UserLogin.CreatePasswordInput;
        }
        public void RemoveCreateUserLogin()
        {
            Window.TextInput -= UserLogin.CreateUsernameInput;

            Window.TextInput -= UserLogin.CreatePasswordInput;
            menuState.inMenu = false;
            menuState.menyMsg = "Now press login";
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Game
            font = Content.Load<SpriteFont>("Fonts/font");
            //visualManager.LoadContent(Content);
            gameState = new GameState(this, _graphics.GraphicsDevice, Content);

            



            //Main Menu
            menuState = new MenuState(this, _graphics.GraphicsDevice, Content);
            currentState = menuState;
            
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
         
            if (nextState != null)
            {
                currentState = nextState;

                nextState = null;
            }

            currentState.Update(gameTime);

            currentState.PostUpdate(gameTime);

            if (currentState == gameState)
            {
                player1.Update();
                player2.Update();

            }
           
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            if (currentState == gameState)
            {
                player1.Draw(_spriteBatch);
                player2.Draw(_spriteBatch);

            }
          

            _spriteBatch.End();

            currentState.Draw(gameTime, _spriteBatch);



            


            base.Draw(gameTime);
        }
    }
}
