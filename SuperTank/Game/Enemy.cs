using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Enemy : IUpdatable, IDisposable
    {
        private Queue<TypeUnit> tankEnemy = new Queue<TypeUnit>(20);
        private Point[] positopn = new Point[]
                {
                    ConfigurationGame.StartPositionTankEnemy1,
                    ConfigurationGame.StartPositionTankEnemy2,
                    ConfigurationGame.StartPositionTankEnemy3
                };
        private int oldPosition;
        private int iterationAddingTank = 0;

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

        public void Clear()
        {
            tankEnemy.Clear();
        }

        public void Start()
        {
            for (int i = 0; i < positopn.Length; i++)
                AddTank(i);

            Game.Updatable.Add(this);
        }

        public void Update()
        {
            if (CountTank() > 0 && Scene.Tanks.Count(t => !((Owner)t.Properties[PropertiesType.Owner] == Owner.Plaeyr)) + Scene.Stars.Count < 3)
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

        private void AddTank(int posIndex)
        {
            iterationAddingTank = 0;
            oldPosition = posIndex;
            bool isBonusTank = false;
            isBonusTank = true; // todo delite it line
            IDriver enemyDriver = new EnemyDriver();
            if (tankEnemy.Count == 17 || tankEnemy.Count == 10 || tankEnemy.Count == 3)
                isBonusTank = true;

            enemyDriver.Tank = FactoryUnit.CreateTank(positopn[posIndex].X, positopn[posIndex].Y, GetTypeTank(), Direction.Down, enemyDriver, isBonusTank);

            Star star = FactoryUnit.CreateStar(TypeUnit.Star, enemyDriver.Tank);
            star.Start();
        }

        public int GetPosition()
        {
            Size size = new Size(ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank);
            
            for (int i = 0, index = oldPosition; i < positopn.Length; i++)
            {
                index = (index >= positopn.Length - 1) ? 0 : index + 1;
                if (IsFreePosition(new Rectangle(positopn[index], size)))
                    return index;
            }
            return -1;
        }

        public bool IsFreePosition(Rectangle recPos)
        {
            for (int i = 0; i < Scene.Tanks.Count; i++)
                if (Scene.Tanks[i].BoundingBox.IntersectsWith(recPos))
                    return false;

            for (int i = 0; i < Scene.Stars.Count; i++)
                if (Scene.Stars[i].BoundingBox.IntersectsWith(recPos))
                    return false;

            return true;
        }

        public void Dispose()
        {
            Game.Updatable.Remove(this);
        }
    }
}
