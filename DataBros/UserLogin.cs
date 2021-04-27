using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DataBros
{
   public class UserLogin
    {
       static public StringBuilder PlayerNameInput = new StringBuilder("UserName");
       static public StringBuilder PasswordInputString = new StringBuilder("Password");
        public static bool pass = false;
        public static bool user = true;
        static KeyboardState releasedKey;
        static KeyboardState pressedKey;


         public static void UsernameInput(object sender, TextInputEventArgs e)
        {
            pressedKey = Keyboard.GetState();


            //pressedKey = e.Key;
            int length = PlayerNameInput.Length;
            if (user == true)
            {
                
                //if (pressedKey == Keys.Back)
                if (pressedKey.IsKeyDown(Keys.Back) && releasedKey.IsKeyUp(Keys.Back))
                {
                    if (length > 0)
                    {
                        PlayerNameInput.Remove(length - 1, 1);
                    }

                }
                else if (!pressedKey.IsKeyDown(Keys.Enter))
                {
                    var character = e.Character;
                    PlayerNameInput.Append(character);
                }

                if (pressedKey.IsKeyDown(Keys.Enter) && releasedKey.IsKeyUp(Keys.Enter))
                {
                    user = false;
                    pass = true;
                    //_game.ChangeState(new GameState(_game, _graphicsDevice, _content));
                    Debug.WriteLine($"{PlayerNameInput}");
                }
                pressedKey = releasedKey;
            }

            
        }

        public static void PasswordInput(object sender, TextInputEventArgs e)
        {
            if (pass == true)
            {
                int length = PasswordInputString.Length;
                if (pressedKey.IsKeyDown(Keys.Back) && releasedKey.IsKeyUp(Keys.Back))
                {
                    if (length > 0)
                    {
                        PasswordInputString.Remove(length - 1, 1);
                    }

                }
                else if (!pressedKey.IsKeyDown(Keys.Enter))
                {
                    var character = e.Character;
                    PasswordInputString.Append(character);
                }
                if (pressedKey.IsKeyDown(Keys.Enter) && releasedKey.IsKeyUp(Keys.Enter))
                {
                    //Debug.WriteLine($"{PasswordInputString}");

                    var playerNameInput = Convert.ToString(PlayerNameInput);
                    var passwordInputString = Convert.ToString(PasswordInputString);
                    Debug.WriteLine($"{passwordInputString}");

                    Player tmpPlayer = new Player();

                    GameWorld.repo.Open();

                    try
                    {
                        GameWorld.repo.FindPlayer($"{playerNameInput}");

                    }
                    catch (Exception)
                    {
                        Debug.WriteLine($"No player found with that name! , Adding player to table");
                        GameWorld.repo.AddPlayer(playerNameInput, 5, passwordInputString);

                    }
                    tmpPlayer = GameWorld.repo.FindPlayer($"{playerNameInput}");
                    GameWorld.repo.Close();

                    // set player
                    if (GameWorld.player1 == null)
                    {
                        GameWorld.player1 = tmpPlayer;

                    }
                    else
                    {
                        GameWorld.player2 = tmpPlayer;
                    }
                    // Reset login
                    GameWorld.menuState.IsCreatingUser = false;
                    pass = false;
                    user = true;
                    GameWorld.Instance.RemoveUserLogin();

                }
            }

        }
    }
}
