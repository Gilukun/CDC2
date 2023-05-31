using System.Diagnostics;

namespace ProjectDurban
{
    internal abstract class Tank
    {
        protected float x;
        protected float y;

        public void ChangePosition(int px, int py)
        {
            x = px;
            y = py;
            Debug.WriteLine("version int");
        }

        public void ChangePosition(float px, float py)
        {
            x = px;
            y = py;
            Debug.WriteLine("version float");
        }

        public abstract void Tire();
    }
}
