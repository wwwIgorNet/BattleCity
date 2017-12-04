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
        private int countTank2Player;
        private int countTankEnemy;
        private bool isTwoPlayer;

        public LevelInfoViewModel(bool isTwoPlayer)
        {
            this.IsTwoPlayer = isTwoPlayer;

            Level = 3;
            CountTank1Player = 2;
            CountTank2Player = 4;
            CountTankEnemy = 17;
        }

        public LevelInfoViewModel() : this(false) { }

        public bool IsTwoPlayer
        {
            get { return isTwoPlayer; }
            set { Set(nameof(IsTwoPlayer), ref isTwoPlayer, value); }
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
    }
}
