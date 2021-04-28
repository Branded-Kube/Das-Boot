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
        public static VisualManager visualManager;
        public static ContentManager content;

        // Ints / points
        private int sizeX = 900;
        private int sizeY = 1100;

        public static Repository repo;



        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;

            //player1 = new Player();
            //player2 = new Player();

            // Database
            var mapper = new Mapper();
            var provider = new SQLiteDatabaseProvider("Data Source=adventurer.db;Version=3;new=true");

            List<Character> result;
            repo = new Repository(provider, mapper);
            repo.Open();

            //repo.AddPlayer("Player1", 100, "Pass1");
            //repo.AddPlayer("Player2", 10, "Pass2");

            repo.AddCharacter("Jon Snow", 2983);
            repo.AddCharacter("Kurt", 2344);
            repo.AddCharacter("Hans", 12);

            repo.AddWater("Lake", 20, true);
            repo.AddWater("Ocean", 100, false);
            repo.AddWater("Stream", 10, true);

            repo.AddFish("Sild", 1, 1);
            repo.AddFish("Tun", 80, 3);
            repo.AddFish("FladFisk", 10, 2);
            repo.AddFish("Torsk", 20, 3);

            result = repo.GetAllCharacters();
            foreach (var character in result)
            {
               Debug.WriteLine($"Id {character.Id} Name {character.Name} XP {character.Experience}");
                
            }

            

            var anotherCharacter = repo.FindCharacter("Jon Snow");
            //Debug.WriteLine($"Id {anotherCharacter.Id} Name {anotherCharacter.Name} XP {anotherCharacter.Experience}");

            repo.Close();
            //

            var mapper1 = new Mapper();
            var provider1 = new SQLiteDatabaseProvider("Data Source=adventurer.db;Version=3;new=true");

            List<Bait> result1;
            var repo1 = new Repository(provider1, mapper1);
            repo.Open();
            
            repo.AddBait("Regnorm", 5, 3);
            repo.AddBait("PowerBait", 10, 1);
            repo.AddBait("Sild", 20, 2);

            result1 = repo.GetAllBait();
            foreach (var bait in result1)
            {
                Debug.WriteLine($"Id {bait.Id} Name {bait.BaitName} Cost {bait.Price}");

            }
            currentBait = repo.FindBait("Regnorm");
            var anotherBait = repo.FindBait("Regnorm");
            Debug.WriteLine($"Id {anotherBait.Id} Name {anotherBait.BaitName} Cost {anotherBait.Price}");

            repo.Close();
        }

        protected override void Initialize()
        {

            // Sets window size
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 1000;

            visualManager = new VisualManager(_spriteBatch, new Rectangle(-566, 0, sizeX, sizeY));

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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Game
            font = Content.Load<SpriteFont>("Fonts/font");
            visualManager.LoadContent(Content);
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

           // if (player1 != null)
           // {
           //     player1.Update();
           // }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();




            _spriteBatch.End();

            currentState.Draw(gameTime, _spriteBatch);





            base.Draw(gameTime);
        }
    }
}
