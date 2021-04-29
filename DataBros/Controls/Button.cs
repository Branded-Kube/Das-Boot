using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DataBros.Controls
{
    public class Button : Component
    {
        #region Fields

        private MouseState currentMouse;

        private MouseState previousMouse;

        private SpriteFont buttonFont;

        private bool hoveringOver;

        public Texture2D buttonTexture;

        #endregion

        #region Properties

        public event EventHandler Click;

        //public bool IsClicked { get; private set; }

        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, buttonTexture.Width, buttonTexture.Height);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        #region Contructor
        public Button(Texture2D texture, SpriteFont font)
        {
            buttonTexture = texture;

            buttonFont = font;

            PenColor = Color.Black;
        }
        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;

            if (hoveringOver)
            {
                color = Color.Gray;
            }

            spriteBatch.Draw(buttonTexture, Rectangle, color);
            
            if (!string.IsNullOrEmpty(Text)) //if there is text in a button, sizes the button to the legth of the text
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (buttonFont.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (buttonFont.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(buttonFont, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            hoveringOver = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                hoveringOver = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
