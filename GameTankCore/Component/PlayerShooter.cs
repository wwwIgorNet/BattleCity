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

        public ICannon Cannon { set { cannon = value; } }

        public void Update()
        {
            if (cannon == null)
                return;

            if (Keyboard.Space)
                cannon.Fire();
        }
    }
}
