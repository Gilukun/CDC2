using Liste_Image;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ListeImages
{
    internal class BubbleList
    {

        List<Bubble> listeBulles = new List<Bubble>();
        //public BubbleList(ContentManager pContent, int pWidth, int pHeight)
        public BubbleList()
        {
            ContentManager Content = ServiceLocator.GetService<ContentManager>();
            ScreenManager screenManager = ServiceLocator.GetService<ScreenManager>();
            Point screenSize = screenManager.GetScreenSize();

            Random rnd = new Random();
            int nBulleX = 0;
            int nBulleY = 0;
            int nBulleRebond = 0;
            //int largeur = pGraphics.GraphicsDevice.Viewport.Width;
            //int hauteur = pGraphics.GraphicsDevice.Viewport.Height;
            ///int largeur = _graphics.PreferredBackBufferWidth; // on peut aussi utiliser le PrefferedBuffer car on l'utilise pour modifier la taille de l'écran
            //int hauteur = _graphics.PreferredBackBufferHeight;


            Bubble uneBulle = new Bubble();
            listeBulles.Add(uneBulle);

            for (int i=1; i<=5; i++)
            {
                Bubble uneBulleX = new bBouge();
                listeBulles.Add(uneBulleX);
            }

            for (int i=1; i<=5; i++)
            {
                Bubble uneBulleDown = new bBougeY();
                listeBulles.Add(uneBulleDown);
            }

            for (int i = 1; i <=5; i++)
            {
                Bubble uneBulleRebond = new bRebond();
                listeBulles.Add(uneBulleRebond);
            }

            foreach (Bubble item in listeBulles)
            {
                if (item is bBouge &&  nBulleX <= 5)
                {
                    item.SetPosition(rnd.Next(screenSize.X - item.width), rnd.Next(screenSize.Y - item.height), -1, 0);
                    nBulleX++;
                    Trace.WriteLine(screenSize.X);
                }

                if (item is bBougeY && nBulleY <= 5)
                {
                    item.SetPosition(rnd.Next(screenSize.X - item.width), rnd.Next(screenSize.Y - item.height), 0, 1);
                    nBulleY++;
                }
                if (item is bRebond && nBulleRebond <= 5)
                {
                    item.SetPosition(rnd.Next(screenSize.X - item.width), rnd.Next(screenSize.Y - item.height), rnd.Next(-1, 1), rnd.Next(-1, 1));
                    nBulleRebond++;
                }

                else
                { item.SetPosition(rnd.Next(screenSize.X - item.width), rnd.Next(screenSize.Y - item.height)); }
            }
        }
        public void Affiche()
        {
        foreach (Bubble item in listeBulles)
                item.Affiche();
        }

        public void Move()
        {

            foreach (Bubble item in listeBulles)
                item.Move(); 
        }

        public void Collisions()
        {
            foreach (Bubble item in listeBulles)
                item.Collisions();
        }
    }


}
