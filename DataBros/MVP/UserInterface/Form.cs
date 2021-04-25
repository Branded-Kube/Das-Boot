//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DataBros.MVP.UserInterface
//{
//    public class Form
//    {
//        //private System.Windows.Forms.TextBox textboxActualValue;
//        //private System.Windows.Forms.Label label1;
//        //private System.Windows.Forms.Label labelVariance;

//        //private System.Windows.Forms.TextBox textboxActualValue;
//        private Vector2 textBoxActualValueSize;
//        private Rectangle textBoxActualValueRectancle;
//        private string textBoxActualValue;
//        private string label1;
//        private string labelVariance;
//        private Texture2D sprite;
        
//        public Form()
//        {
//            textBoxActualValueSize = new Vector2(10, 10);
//            textBoxActualValueRectancle = new Rectangle(Convert.ToInt32(textBoxActualValueSize.X), Convert.ToInt32(textBoxActualValueSize.Y) , 10, 10);
//            GameWorld.content.Load<Texture2D>("upgrademenu");
//        }

//        public void Update()
//        {

//        }
//        public void Draw(SpriteBatch spriteBatch)
//        {
//            spriteBatch.Draw(sprite, textBoxActualValueRectancle, Color.White);
//        }


//    }
//}
