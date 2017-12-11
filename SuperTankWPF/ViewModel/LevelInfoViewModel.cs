using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class LevelInfoViewModel : ObservableObject
    {
        private int level;
        private int countTank1Player;
        private int countTank2Player = -1;
        private int countTankEnemy;
        private bool isTwoPlayer;

        public bool IsTwoPlayer
        {
            get { return isTwoPlayer; }
            set
            {
                if (value == false) CountTank2Player = -1;
                Set(nameof(IsTwoPlayer), ref isTwoPlayer, value);
            }
        }
        public int Level
        {
            get { return level; }
            set { Set(nameof(Level), ref level, value); }
        }
        public int CountTank1Player
        {
            get { return countTank1Player; }
            set { Set(nameof(CountTank1Player), ref countTank1Player, value); }
        }
        public int CountTank2Player
        {
            get { return countTank2Player; }
            set { Set(nameof(CountTank2Player), ref countTank2Player, value); }
        }
        public int CountTankEnemy
        {
            get { return countTankEnemy; }
            set { Set(nameof(CountTankEnemy), ref countTankEnemy, value); }
        }

        public void Cler()
        {
            CountTankEnemy = 0;
            CountTank2Player = -1;
            countTank1Player = 0;
            Level = 0;
        }
    }
}
