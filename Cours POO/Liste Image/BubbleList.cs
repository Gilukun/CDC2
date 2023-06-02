using Liste_Image;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
            Bubble uneBulleRouge = new bBouge(pContent);
            Bubble uneBulleDown = new bBougeY(pContent);
            Bubble uneBulleRebond = new bRebond(pContent);
            
            listeBulles.Add(uneBulleRouge);
            listeBulles.Add(uneBulle);
            listeBulles.Add(uneBulleDown);
            listeBulles.Add(uneBulleRebond);

            foreach (Bubble item in listeBulles)
            {
                for (int i = 1; i <= 10; i++)
                {
                    int largeur = pGraphics.GraphicsDevice.Viewport.Width;
                    int hauteur = pGraphics.GraphicsDevice.Viewport.Height;

                    item.SetPosition(rnd.Next(largeur - item.width), rnd.Next(hauteur - item.height));
                }

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
