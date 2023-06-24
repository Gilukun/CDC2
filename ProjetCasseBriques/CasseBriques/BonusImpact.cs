using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasseBriques.Bonus;

namespace CasseBriques
{
    internal class BonusImpact : Bonus
    {
        public BonusImpact(Texture2D pTexture) : base(pTexture)
        {
        
            AddBonus = 2;
            currentState = BonusState.Idle;
            Speed = 0.05f;
            Vitesse = new Vector2(0, 1);
    }
    public override void SetPositionBonus(float pX, float pY)
    {
        Position = new Vector2(pX, pY);
    }
    public override void Tombe()
    {
        Vitesse = new Vector2(Vitesse.X, Vitesse.Y + Speed);
    }
    public override void Update()

    {
        if (currentState == BonusState.Free)
        {
            Tombe();
        }

        base.Update();
    }


}
}
