using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace DataBros
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Database
            var mapper = new AdventurerMapper();
            var provider = new SQLiteDatabaseProvider("Data Source=adventurer.db;Version=3;new=true");

            List<Character> result;
            var repo = new Repository(provider, mapper);
            repo.Open();

            repo.AddCharacter("Jon Snow", 2983);
            repo.AddCharacter("Kurt", 2344);
            repo.AddCharacter("Hans", 12);

            result = repo.GetAllCharacters();
            foreach (var character in result)
            {
                Debug.WriteLine($"Id {character.Id} Name {character.Name} XP {character.Experience}");
                
            }

            var anotherCharacter = repo.FindCharacter("Jon Snow");
            Debug.WriteLine($"Id {anotherCharacter.Id} Name {anotherCharacter.Name} XP {anotherCharacter.Experience}");

            repo.Close();
            //
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
