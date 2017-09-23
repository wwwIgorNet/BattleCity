using System;

namespace SuperTank
{
    class Bonus : UpdatableUnit
    {
        private DateTime starTime;
        public Bonus(int id, int x, int y, int width, int height, TypeUnit type) : base(id, x, y, width, height, type)
        {
        }

        public override void Start()
        {
            base.Start();
            AddToScene();
            Scene.Bonus.Add(this);
            starTime = DateTime.Now;
        }
        public override void Dispose()
        {
            base.Dispose();
            Scene.Bonus.Remove(this);
        }
        public override void Update()
        {
            if (DateTime.Now - starTime > TimeSpan.FromSeconds(10))
                Dispose();
        }
    }
}
