using System.Diagnostics;

namespace ProjectDurban
{
    internal class NormalTank : Tank
    {
        public override void Tire()
        {
            Debug.WriteLine("Il tire au canon");
        }
    }
}
