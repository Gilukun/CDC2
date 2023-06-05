using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Template
{
    public interface IActor
    {
        // On indique uniquement ce que l'actor doit récupérer. On ne doit pas coder de fonction etc...

        Vector2 Position { get; }  // on ne peut pas passer une variable on doit passer  une méthode. Ici on passe une property {get;} pour dire qu'on récupère la position
        Rectangle BoundingBox { get; } // espace occupé par les acteurs.  
        void Update(GameTime pGametime); 
        void Draw(SpriteBatch pSpriteBatch);
        void TouchBy(IActor pBy); // fonction pour la réaction lorsqu'un acteur est touché
    }
}
