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
        static Player tmpPlayer;

         public static void CreateUsernameInput(object sender, TextInputEventArgs e)
        {
            pressedKey = Keyboard.GetState();

            int length = PlayerNameInput.Length;
            if (user == true)
            {
                
                if (pressedKey.IsKeyDown(Keys.Back) && releasedKey.IsKeyUp(Keys.Back))
                {
                    if (length > 0)
                    {
                        PlayerNameInput.Remove(length - 1, 1);
                    }

                }
                else if (e.Key != Keys.Enter)
                {
                    var character = e.Character;
                    PlayerNameInput.Append(character);
                }

                if (pressedKey.IsKeyDown(Keys.Enter) && releasedKey.IsKeyUp(Keys.Enter))
                {
                    user = false;
                    pass = true;
                    Debug.WriteLine($"{PlayerNameInput}");
                }
                pressedKey = releasedKey;
            }

            
        }

        public static void CreatePasswordInput(object sender, TextInputEventArgs e)
        {

            if (pass == true)
            {
                GameWorld.menuState.menyMsg = "Write your password and Press enter to confirm";
                int length = PasswordInputString.Length;
                if (pressedKey.IsKeyDown(Keys.Back) && releasedKey.IsKeyUp(Keys.Back))
                {
                    if (length > 0)
                    {
                        PasswordInputString.Remove(length - 1, 1);
                    }
                }
                else if (e.Key != Keys.Enter)
                {
                        var character = e.Character;
                        PasswordInputString.Append(character);
                }
                if (pressedKey.IsKeyDown(Keys.Enter) && releasedKey.IsKeyUp(Keys.Enter))
                {
                    var playerNameInput = Convert.ToString(PlayerNameInput);
                    var passwordInputString = Convert.ToString(PasswordInputString);
                    Debug.WriteLine($"{passwordInputString}");

                    GameWorld.repo1.Open();
                    bool success = false;
                    try
                    {
                        GameWorld.repo1.FindPlayer($"{playerNameInput}");
                        success = true;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine($"No player found with that name! , Adding player to table");
                        GameWorld.repo1.AddPlayer(playerNameInput,0,$"{PasswordInputString}");

                    }
                    finally
                    {
                        if (success)
                        {
                            Debug.WriteLine($"Player already exists! try another name");
                        }
                    }
                   
                    GameWorld.repo1.Close();

                    // Reset login
                    GameWorld.menuState.IsCreatingUser = false;
                    pass = false;
                    user = true;
                    GameWorld.Instance.RemoveCreateUserLogin();

                }
                pressedKey = releasedKey;

            }

        }

        internal static void UsernameInput(object sender, TextInputEventArgs e)
        {
            pressedKey = Keyboard.GetState();
            int length = PlayerNameInput.Length;
            if (user == true)
            {

                if (pressedKey.IsKeyDown(Keys.Back) && releasedKey.IsKeyUp(Keys.Back))
                {
                    if (length > 0)
                    {
                        PlayerNameInput.Remove(length - 1, 1);
                    }

                }
                else if (e.Key != Keys.Enter)
                {
                    var character = e.Character;
                    PlayerNameInput.Append(character);
                }

                if (pressedKey.IsKeyDown(Keys.Enter) && releasedKey.IsKeyUp(Keys.Enter))
                {
                    tmpPlayer = new Player();
                    var playerNameInput = Convert.ToString(PlayerNameInput);

                    GameWorld.repo1.Open();

                    try
                    {
                        tmpPlayer = GameWorld.repo1.FindPlayer($"{playerNameInput}");
                        Debug.WriteLine($"Player Found! {tmpPlayer.Name}");
                        user = false;
                        pass = true;
                    }
                    catch (Exception)
                    {
                        GameWorld.menuState.menyMsg = "No player found with that name!";
                        GameWorld.menuState.IsCreatingUser = false;
                        GameWorld.Instance.RemoveUserLogin();

                    }
                    GameWorld.repo1.Close();
                    
                }
                pressedKey = releasedKey;
            }


        }

        internal static void PasswordInput(object sender, TextInputEventArgs e)
        {
            if (pass == true)
            {
                GameWorld.menuState.menyMsg = "Write your password and Press enter to login";
                int length = PasswordInputString.Length;
                if (pressedKey.IsKeyDown(Keys.Back) && releasedKey.IsKeyUp(Keys.Back))
                {
                    if (length > 0)
                    {
                        PasswordInputString.Remove(length - 1, 1);
                    }

                }
                else if (e.Key != Keys.Enter)
                {
                    var character = e.Character;
                    PasswordInputString.Append(character);
                }
                
                if (pressedKey.IsKeyDown(Keys.Enter) && releasedKey.IsKeyUp(Keys.Enter))
                {
                    var passwordInputString = Convert.ToString(PasswordInputString);
                    Debug.WriteLine($"{passwordInputString}");

                   

                    if (tmpPlayer.Password == passwordInputString)
                    {
                        Debug.WriteLine($"Password found! {tmpPlayer.Password}");

                        // set players
                        if (GameWorld.Instance.player1.logedIn == false)
                        {
                            GameWorld.Instance.player1 = tmpPlayer;
                            GameWorld.Instance.player1.logedIn = true;
                            Debug.WriteLine($"Setting Player1 to {GameWorld.Instance.player1.Name}");
                            GameWorld.Instance.player1.isplayer1 = true;
                            GameWorld.Instance.player1.Loadcontent();
                            GameWorld.menuState.menyMsg = "Now login with player 2";

                        }
                        else if (GameWorld.Instance.player2.logedIn == false)
                        {
                            if (GameWorld.Instance.player1.Name != tmpPlayer.Name)
                            {
                                GameWorld.Instance.player2 = tmpPlayer;
                                GameWorld.Instance.player2.logedIn = true;
                                Debug.WriteLine($"Setting Player2 to {GameWorld.Instance.player2.Name}");
                                GameWorld.Instance.player2.isplayer1 = false;
                                GameWorld.Instance.player2.Loadcontent();
                                GameWorld.menuState.menyMsg = "Now press start to start fishing";

                            }
                            else
                            {
                                GameWorld.menuState.menyMsg = "User already logged in!! try with another user ?";
                            }

                        }
                        else if (GameWorld.Instance.player2.logedIn == true & GameWorld.Instance.player1.logedIn == true)
                        {
                            GameWorld.menuState.menyMsg = "Only 2 players are allowed, press start to start fishing";
                        }


                    }
                    else
                    {
                        GameWorld.menuState.menyMsg = "Wrong Password";
                    }

                    // Reset login
                    GameWorld.menuState.IsCreatingUser = false;
                    pass = false;
                    user = true;
                    GameWorld.Instance.RemoveUserLogin();

                }
                pressedKey = releasedKey;

            }
        }
    }
}
