using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
    public delegate void OnClick(Boutons pSender); // delegate permet d'avoir juste la signature de la fonction. Et on peut ensuite utiliser un peu comme une variable plus bas
    public class Boutons : Sprites
    {
        protected bool isHover { get; private set;} // les autres peuvent prendre la valeur mais pas la changer
        protected MouseState oldMState;
        public OnClick onClick { get; set; }
        public Boutons(Texture2D pTexture) : base(pTexture)
        {

        }

        public override void Update(GameTime pGameTime)
        {
            MouseState newMState = Mouse.GetState(); 
            Point MousePos = Mouse.GetState().Position;

            if (BoundingBox.Contains(MousePos))  //est que la boundingbox contient la position de la souris
            { 
                if (!isHover) // si on ne survole pas déjà le bouton, on passe isHover en true
                {
                    isHover = true;
                    Trace.WriteLine("Je survole le bouton"); 
                }
            
            }
            else
            {
                if(isHover)
                {
                    Trace.WriteLine("Je ne survole plus le bouton");
                }
            }

            // Si la souris passe sur le bouton on fait quoi ? 

            if (isHover)
            {
                if (newMState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released) 
                {
                    Trace.WriteLine("Je clique");
                    if (onClick != null)
                        onClick(this);
                
                }
            }



            oldMState = newMState;
            base.Update(pGameTime);

        }
    }
}
