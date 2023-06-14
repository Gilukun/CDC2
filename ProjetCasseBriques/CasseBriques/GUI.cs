using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBriques
{
    public class GUI : Sprites
    {
        public delegate void OnClick(GUI pSender);
        protected bool isHover { get; private set; }
        protected MouseState oldMState;
        protected MouseState newMState;
        protected Point mousePosition;
        public OnClick onClick { get; set; }
        public GUI(Texture2D pTexture) : base(pTexture)
        {
        }

        public override void Update()
        {
            newMState = Mouse.GetState();
            mousePosition = Mouse.GetState().Position;
            
            if (BoundingBox.Contains(mousePosition))
            {
                if (!isHover)
                {
                    isHover = true;
                    Trace.WriteLine("Je survole mon bouton");
                }
            }
            else
            {
                if (isHover)
                { 
                    isHover= false;
                }
            }

            if (isHover)
            {
                if (newMState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)
                {
                    Trace.WriteLine("Je clique");
                    if (onClick != null) 
                    {
                        onClick(this);
                    }
                }
            }
            oldMState = newMState;

            base.Update();
        }



    }
}
