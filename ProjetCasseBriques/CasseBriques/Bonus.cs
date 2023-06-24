using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasseBriques.Personnages;

namespace CasseBriques
{
    public class Bonus : Sprites
    {

        public Texture2D texture;
        protected HUD HUD;
        public BonusVie Vie;
        public int AddBonus;

        public enum BonusState
        {
            Idle,
            Free,
            Falling,
            Catch
        }
        public BonusState currentState { get; set; }

        public Bonus(Texture2D pTexture) : base(pTexture)
        {
            texture = pTexture;
        }
        public override void Load()
        {
            base.Load();
        }
        public virtual void AddLife()
        { 
        }

        public virtual void RemoveLife() 
        { 
        }
        public virtual void Tombe()
        {

        }

        public override void Update()
        { 
            base.Update();
        }

        public virtual void SetPositionBonus(float pX, float pY)
        {
            Position = new Vector2(pX, pY);
        }
    }
}
