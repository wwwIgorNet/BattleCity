using System;
using System.Collections.Generic;
using System.Drawing;

namespace SuperTank
{
    /// <summary>
    /// Bonus, shovel. Surrounds the eagle with a concrete wall
    /// </summary>
    class ShovelBonus : UpdatableBase
    {
        private static DateTime startTime;
        private Size size = new Size(ConfigurationGame.WidthTile, ConfigurationGame.HeightTile);
        // Coordinates of the concrete wall around the eagle
        private Point[] position = new Point[]
        {
            new Point(ConfigurationGame.PositionEagle.X - 20, ConfigurationGame.PositionEagle.Y + 20),
            new Point(ConfigurationGame.PositionEagle.X - 20, ConfigurationGame.PositionEagle.Y),
            new Point(ConfigurationGame.PositionEagle.X - 20, ConfigurationGame.PositionEagle.Y - 20),
            new Point(ConfigurationGame.PositionEagle.X, ConfigurationGame.PositionEagle.Y - 20),
            new Point(ConfigurationGame.PositionEagle.X + 20, ConfigurationGame.PositionEagle.Y - 20),
            new Point(ConfigurationGame.PositionEagle.X + 40, ConfigurationGame.PositionEagle.Y - 20),
            new Point(ConfigurationGame.PositionEagle.X + 40, ConfigurationGame.PositionEagle.Y),
            new Point(ConfigurationGame.PositionEagle.X + 40, ConfigurationGame.PositionEagle.Y + 20)

        };
        // List of units of the concrete wall around the eagle
        private List<Unit> unitsAroundEagle = new List<Unit>();
        // Number of calls Update
        private int iteration = 0;

        public override void Start()
        {
            base.Start();
            FindUnitsAroundEagle();

            startTime = DateTime.Now;
            RemoveUnitAroundEagle();
            AddUnitAroundEagle(TypeUnit.ConcreteWall);
        }
        public override void Dispose()
        {
            base.Dispose();
            RemoveUnitAroundEagle();
            AddUnitAroundEagle(TypeUnit.BrickWall);
        }
        public override void Update()
        {
            TimeSpan period = DateTime.Now - startTime;
            if(period > ConfigurationGame.TimeShovelBonus)
            {
                Dispose();
            }
            // The last 4 seconds, alternately changing the concrete wall and brick
            else if (period > ConfigurationGame.TimeShovelBonus - TimeSpan.FromSeconds(4))
            {
                if (iteration == 15)
                {
                    RemoveUnitAroundEagle();
                    AddUnitAroundEagle(TypeUnit.ConcreteWall);
                    iteration++;
                }
                else if (iteration == 25)
                {
                    RemoveUnitAroundEagle();
                    AddUnitAroundEagle(TypeUnit.BrickWall);
                    iteration = 0;
                }
                else iteration++;
            }
        }

        /// <summary>
        /// Add units around eagle
        /// </summary>
        /// <param name="type">Type unit than add</param>
        private void AddUnitAroundEagle(TypeUnit type)
        {
            unitsAroundEagle.Clear();
            foreach (Point pos in position)
            {
                Unit u = FactoryUnit.CreateUnit(pos.X, pos.Y, type);
                unitsAroundEagle.Add(u);
            }
            Scene.AddRange(unitsAroundEagle);
        }
        /// <summary>
        /// Remove units around eagle
        /// </summary>
        private void RemoveUnitAroundEagle()
        {
            foreach (Unit unit in unitsAroundEagle)
                unit.Dispose();
        }
        /// <summary>
        /// Find units around eagle
        /// </summary>
        private void FindUnitsAroundEagle()
        {
            Rectangle rec = new Rectangle(0, 0, size.Width, size.Height);
            for (int i = 0; i < Scene.Units.Count; i++)
            {
                foreach (Point pos in position)
                {
                    rec.Location = pos;
                    if (rec == Scene.Units[i].BoundingBox)
                        unitsAroundEagle.Add(Scene.Units[i]);
                }
            }
        }
    }
}
