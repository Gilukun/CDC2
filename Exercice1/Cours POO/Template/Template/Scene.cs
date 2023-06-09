﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
     abstract public class Scene
    {
        protected MainGame mainGame;
        protected List<IActor> listActors;
        public Scene(MainGame pGame) 
        { 
            mainGame = pGame; 
            listActors = new List<IActor>();
        }

        // Fonction pour supprimer les IActor de la liste 
        public void Clean()
        {
            listActors.RemoveAll(item => item.ToRemove == true); // On supprime de la liste uniquement les items qui ont la propriété ToRemove= true
        }
        public virtual void Load()
        {
            
        }
        public virtual void Unload()
        { 
        
        }

        public virtual void Update(GameTime gameTime) 
        {
            foreach (IActor actor in listActors) 
            { 
                actor.Update(gameTime);
            }
        
        }

        public virtual void Draw(GameTime gameTime) 
        {
            foreach (IActor actor in listActors)
            {
                actor.Draw(mainGame._spriteBatch);
            }

        } 

    }
}
