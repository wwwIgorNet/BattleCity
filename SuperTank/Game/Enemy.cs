using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Enemy
    {
        private Queue<TypeUnit> tankEnemy = new Queue<TypeUnit>(10);

        public void AddTank(TypeUnit tankType)
        {
            tankEnemy.Enqueue(tankType);
        }

        public TypeUnit GetTank()
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
    }
}
