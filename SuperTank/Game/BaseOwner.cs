using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public abstract class BaseOwner : UpdatableBase
    {
        public void Stop()
        {
            LevelManager.Updatable.Remove(this);
        }
    }
}
