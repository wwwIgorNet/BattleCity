namespace SuperTank
{
    /// <summary>
    /// Big detonation(tank, eagle)
    /// </summary>
    class BigDetonation : UpdatableUnit
    {
        private int iterationUpdate;

        public BigDetonation(int id, int x, int y, int width, int height, TypeUnit type) : base(id, x, y, width, height, type)
        { }

        public override void Update()
        {
            iterationUpdate++;
            if (iterationUpdate == ConfigurationGame.TimeBigDetonation)
                Dispose();
        }
        public override void Start()
        {
            base.Start();
            AddToScene();
        }
    }
}
