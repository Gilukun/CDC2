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
        public BubbleList(ContentManager pContent, GraphicsDeviceManager pGraphics)
        {
            Random rnd = new Random();

            Bubble uneBulle = new Bubble(pContent);
            listeBulles.Add(uneBulle);

            for (int i=1; i<=5; i++)
            {
                Bubble uneBulleX = new bBouge(pContent);
                listeBulles.Add(uneBulleX);
            }

            for (int i=1; i<=6; i++)
            {
                Bubble uneBulleDown = new bBougeY(pContent);
                listeBulles.Add(uneBulleDown);
            }

            for (int i = 1; i <=8; i++)
            {
                Bubble uneBulleRebond = new bRebond(pContent);
                listeBulles.Add(uneBulleRebond);
            }

            int nBulleX = 0;
            int nBulleY = 0;
            int nBulleRebond = 0;

            foreach (Bubble item in listeBulles)
            {
                    int largeur = pGraphics.GraphicsDevice.Viewport.Width;
                    int hauteur = pGraphics.GraphicsDevice.Viewport.Height;
               
                if (item is bBouge &&  nBulleX <= 5)
                {
                    
                    item.SetPosition(rnd.Next(largeur - item.width), rnd.Next(hauteur - item.height), rnd.Next(-1, 1 + 1), 0);
                    nBulleX++;
                }

                if (item is bBougeY && nBulleY <= 6)
                {

                    item.SetPosition(rnd.Next(largeur - item.width), rnd.Next(hauteur - item.height), 0, rnd.Next(-1, 1 + 1));
                    nBulleY++;
                }
                if (item is bRebond && nBulleY <= 8)
                {

                    item.SetPosition(rnd.Next(largeur - item.width), rnd.Next(hauteur - item.height), rnd.Next(-1, 1 + 1), rnd.Next(-1, 1 + 1));
                    nBulleRebond++;
                }

                else
                { item.SetPosition(rnd.Next(largeur - item.width), rnd.Next(hauteur - item.height)); }
            }
        }
        public void Affiche(SpriteBatch spriteBatch) 
        {
        foreach (Bubble item in listeBulles)
                item.Affiche(spriteBatch);
        }

        public void Move(GraphicsDeviceManager pGraphics )
        {
            foreach (Bubble item in listeBulles)
                item.Move(pGraphics); 
        }
       

        public void Collisions(GraphicsDeviceManager pGraphics)
        {
            foreach (Bubble item in listeBulles)
                item.Collisions(pGraphics);
        }
    }


}
