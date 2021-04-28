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


         public static void CreateUsernameInput(object sender, TextInputEventArgs e)
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

                    GameWorld.repo.Open();
                    bool success = false;
                    try
                    {
                        GameWorld.repo.FindPlayer($"{playerNameInput}");
                        success = true;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine($"No player found with that name! , Adding player to table");
                        GameWorld.repo.AddPlayer(playerNameInput,0,$"{PasswordInputString}");

                    }
                    finally
                    {
                        if (success)
                        {
                            Debug.WriteLine($"Player already exists! try another name");
                        }
                    }
                   
                    //tmpPlayer = GameWorld.repo.FindPlayer($"{playerNameInput}");
                    GameWorld.repo.Close();

                    // set player
                    //if (GameWorld.player1 == null)
                    //{
                    //    GameWorld.player1 = tmpPlayer;

                    //}
                    //else
                    //{
                    //    GameWorld.player2 = tmpPlayer;
                    //}
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
                    user = false;
                    pass = true;
                    Debug.WriteLine($"{PlayerNameInput}");
                }
                pressedKey = releasedKey;
            }


        }

        internal static void PasswordInput(object sender, TextInputEventArgs e)
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

                    Player tmpPlayer = new Player();

                    GameWorld.repo.Open();

                    try
                    {
                        tmpPlayer = GameWorld.repo.FindPlayer($"{playerNameInput}");
                       Debug.WriteLine($"Player Found! {tmpPlayer.Name}");

                    }
                    catch (Exception)
                    {
                        Debug.WriteLine($"No player found with that name!");
                    }
                    GameWorld.repo.Close();

                    if (tmpPlayer.Password == passwordInputString)
                    {
                        Debug.WriteLine($"Password found! {tmpPlayer.Password}");

                        // set players
                        if (GameWorld.Instance.player1 == null)
                        {
                            GameWorld.Instance.player1 = tmpPlayer;
                            Debug.WriteLine($"Setting Player1 to {GameWorld.Instance.player1.Name}");
                        }
                        else if (GameWorld.Instance.player2 == null)
                        {
                            if (GameWorld.Instance.player1.Name != tmpPlayer.Name)
                            {
                                GameWorld.Instance.player2 = tmpPlayer;
                                Debug.WriteLine($"Setting Player2 to {GameWorld.Instance.player2.Name}");
                            }
                            else
                            {
                                Debug.WriteLine($"Same user as player 1 and 2 ! try with another user ?");
                            }

                        }
                        else if (GameWorld.Instance.player2 != null & GameWorld.Instance.player1 != null)
                        {
                            Debug.WriteLine($" Only 2 players are allowed");
                        }


                    }
                    else
                    {
                        Debug.WriteLine($"Wrong Password");
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
