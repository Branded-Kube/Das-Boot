using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DataBros
{
   public class VisualManager
    {
        #region Fields & Properties
        private Rectangle displayRectangle;

        //Handeling of nodes


        //Handeling of cells
        public int cellCount;


        //Collections
        public List<Cell> grid;
      
        

        #endregion


        #region Constructor
        public VisualManager(SpriteBatch spriteBatch, Rectangle displayRectangle)
        {
            this.displayRectangle = displayRectangle;


            cellCount = 11;
            

            CreateGrid();
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Cell cell in grid)
            {
                cell.Draw(spriteBatch);
            }

        }
       


        /// <summary>
        /// creates a grid from the ammount of cells we have
        /// </summary>
        public void CreateGrid()
        {
            grid = new List<Cell>();

            grid.Clear();

            int cellSize = displayRectangle.Width / cellCount;

            for (int x = 0; x < cellCount; x++)
            {
                for (int y = 0; y < cellCount; y++)
                {
                    grid.Add(new Cell(new Point(x, y), cellSize));
                }
            }
        }
       

        /// <summary>
        /// Loads / sets cell textures
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            
        }
        #endregion
    }
}
