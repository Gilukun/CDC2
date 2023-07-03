using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class Settings : ScenesManager
    {
        AssetsManager assets = ServiceLocator.GetService<AssetsManager>();  
        Texture2D background;
        private int[][] grid;
        private int id;
        Settings icons;
        List<Settings> listIcons = new List<Settings>();

        public Settings()
        {
            int colNb = 5;
            int linNb = 5;
            id = 0;
            grid = new int[linNb][];
            for (int l = 0; l < linNb; l++)
            {
                grid[l] = new int[colNb];
                for (int c = 0; c < colNb; c++)
                {
                    grid[l][c] = id;
                    id++;
                }
            }
            //for (int l = 0; l < linNb; l++)
            //{
            //    grid[l] = new int[colNb];
            //    for (int c = 0; c < colNb; c++)
            //    {
            //        int typeIcons = icons.grid[l][c];

            //        switch (typeIcons)
            //        {
            //            case 1:

            //            case 2:


            //            case 3:


            //            default:
            //                break;
            //        }
            //    }
            //}
        }

        public override void Load()
        {
        }
        public override void Update()
        {
            base.Update();
        }

        public override void DrawScene()
        {
            SpriteBatch pBatch = ServiceLocator.GetService<SpriteBatch>();
            //pBatch.Draw(background, new Vector2(0, 0), Color.White);

            int colNb = 10;
            int linNb = 10;
            int gridWidth = Screen.Width / colNb;
            int gridHeight = Screen.Height / linNb;
            int[][] gridNb = new int[linNb][];
            id = 0;
            for (int l = 0; l < linNb; l++)
            {
                gridNb[l] = new int[colNb];
                for (int c = 0; c < colNb; c++)
                {
                    gridNb[l][c] = id;
                    id++;
                    pBatch.DrawString(assets.PopUpFont, id.ToString(), new Vector2(c*gridWidth, l*gridHeight), Color.White) ;

                }
            }
        }
    }
}
