using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class PlayerShooter : IShooter
    {
        private ICannon cannon;

        public PlayerShooter(ICannon cannon)
        {
            this.cannon = cannon;
        }

        public void Update()
        {
            if (Keyboard.Space)
                cannon.Fire();
        }
    }
}
