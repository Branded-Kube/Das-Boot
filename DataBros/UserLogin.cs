using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBros
{
   static class UserLogin
    {
       static public StringBuilder PlayerNameInput = new StringBuilder("UserName");
       static public StringBuilder PasswordInputString = new StringBuilder("Password");
        public static bool pass = false;
        public static bool user = true;

        static public void UsernameInput(object sender, TextInputEventArgs e)
        {
            var pressedKey = e.Key;
            int length = PlayerNameInput.Length;
            if (user == true)
            {
                if (pressedKey == Keys.Back)
                {
                    if (length > 0)
                    {
                        PlayerNameInput.Remove(length - 1, 1);
                    }

                }
                else if (pressedKey != Keys.Tab)
                {
                    var character = e.Character;
                    PlayerNameInput.Append(character);
                }

                if (pressedKey == Keys.Enter)
                {
                    pass = true;
                    user = false;
                    _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
                }
            }

        }

      static  public void PasswordInput(object sender, TextInputEventArgs e)
        {
            if (pass == true)
            {
                var pressedKey = e.Key;
                int length = PasswordInputString.Length;
                if (pressedKey == Keys.Back)
                {
                    if (length > 0)
                    {
                        PasswordInputString.Remove(length - 1, 1);
                    }

                }
                else if (pressedKey != Keys.Tab)
                {
                    var character = e.Character;
                    PasswordInputString.Append(character);
                }
            }
            

            
        }


        



    }
}
