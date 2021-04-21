using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DataBros
{
    //enum CellType { START, GOAL, WALL, EMPTY };

    public class Cell
    {
        #region Fields & Properties
        private Texture2D sprite;
        public Texture2D Sprite { get { return sprite; } set { sprite = value; } }
       
        private Point myPos;

        private int cellSize;

        private bool walkAble;

        public bool WalkAble
        {
            get { return walkAble; }
            set { walkAble = value; }
        }



        private Color myColor;

        public Color MyColor
        {
            get { return myColor; }
            set { myColor = value; }
        }

        public Point MyPos
        {
            get { return myPos; }
            set { myPos = value; }
        }


        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(myPos.X * cellSize, myPos.Y * cellSize, cellSize, cellSize);
            }
        }
        #endregion

        #region Constructor
        public Cell(Point pos, int size)
        {
            this.myPos = pos;
            
            this.cellSize = size;

            walkAble = true;
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
            {
                spriteBatch.Draw(sprite, BoundingRectangle, MyColor);
            }
           

            spriteBatch.DrawString(GameWorld.font, string.Format("{0}", myPos), new Vector2(myPos.X * cellSize, (myPos.Y * cellSize)), MyColor);
        }
        #endregion
    }
}
