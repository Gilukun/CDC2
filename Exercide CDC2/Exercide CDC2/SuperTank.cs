using System.Diagnostics;

namespace ProjectDurban
{
    internal class SuperTank : Tank
    {
        public override void Tire()
        {
            Debug.WriteLine("Il tire au laser");
        }
    }
}
