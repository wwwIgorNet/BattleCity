using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTank
{
    public class Enemy : BaseOwner
    {
        private IGameInfo gameInfo;
        private int countRemoveTank;
        private Queue<TypeUnit> tankEnemy = new Queue<TypeUnit>(20);
        private Point[] positopn = new Point[]
                {
                    ConfigurationGame.StartPositionTankEnemy1,
                    ConfigurationGame.StartPositionTankEnemy2,
                    ConfigurationGame.StartPositionTankEnemy3
                };
        private int oldPosition;
        private int iterationAddingTank = 0;
        public event Action RemoveAllTank;

        public Enemy(IGameInfo gameInfo)
        {
            this.gameInfo = gameInfo;
        }

        public void AddTypeTank(TypeUnit tankType)
        {
            tankEnemy.Enqueue(tankType);
        }

        public TypeUnit GetTypeTank()
        {
            return tankEnemy.Dequeue();
        }

        public int CountTank()
        {
            return tankEnemy.Count;
        }

        public override void Start()
        {
            base.Start();
            for (int i = 0; i < positopn.Length; i++)
                AddTank(i);
        }

        public override void Update()
        {
            if (CountTank() > 0 && Scene.Tanks.Count(t => !((Owner)t.Properties[PropertiesType.Owner] == Owner.Player)) + Scene.Stars.Count < 3)
            {
                if (iterationAddingTank > ConfigurationGame.DelayAddingTank)
                {
                    int posIndex = GetPosition();
                    if (posIndex == -1) return;

                    AddTank(posIndex);
                }
                else iterationAddingTank++;
            }
        }

        internal void Clear()
        {
            tankEnemy.Clear();
            countRemoveTank = 0;
        }

        private void AddTank(int posIndex)
        {
            iterationAddingTank = 0;
            oldPosition = posIndex;
            bool isBonusTank = false;
            IDriver enemyDriver = new EnemyDriver();
            if (tankEnemy.Count == 17 || tankEnemy.Count == 10 || tankEnemy.Count == 3)
                isBonusTank = true;

            enemyDriver.Tank = FactoryUnit.CreateTank(positopn[posIndex].X, positopn[posIndex].Y, GetTypeTank(), Direction.Down, enemyDriver, isBonusTank);
            enemyDriver.Tank.UnitDisposable += u => 
            {
                countRemoveTank++;
                if (countRemoveTank == ConfigurationGame.CountTankEnemy)
                    RemoveAllTank.Invoke();
            };

            Star star = FactoryUnit.CreateStar(TypeUnit.Star, enemyDriver.Tank);
            star.Start();

            gameInfo.SetCountTankEnemy(tankEnemy.Count);
        }

        public int GetPosition()
        {
            Size size = new Size(ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank);
            
            for (int i = 0, index = oldPosition; i < positopn.Length; i++)
            {
                index = (index >= positopn.Length - 1) ? 0 : index + 1;
                if (Scene.IsFreePosition(new Rectangle(positopn[index], size)))
                    return index;
            }
            return -1;
        }
    }
}
