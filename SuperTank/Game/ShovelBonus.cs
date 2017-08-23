using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class ShovelBonus : IUpdatable, IDisposable
    {
        private Size size = new Size(ConfigurationGame.WidthTile, ConfigurationGame.HeightTile);
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
        private List<Unit> unitsAroundEagle = new List<Unit>();
        private readonly int DELAY_SHOVEL_BONUS = ConfigurationGame.DelayShovelBonus;
        private int iteration = 0;

        public void Start()
        {
            Game.Updatable.Add(this);
            GetUnitsAroundEagle();

            RemoveUnitAroundEagle();
            AddUnitAroundEagle(TypeUnit.ConcreteWall);
        }

        public void Dispose()
        {
            Game.Updatable.Remove(this);
            RemoveUnitAroundEagle();
            AddUnitAroundEagle(TypeUnit.BrickWall);
        }

        public void Update()
        {
            if (iteration > DELAY_SHOVEL_BONUS)
            {
                Dispose();
            }
            else iteration++;
        }

        private void AddUnitAroundEagle(TypeUnit type)
        {
            unitsAroundEagle.Clear();
            foreach (Point pos in position)
            {
                Unit u = FactoryUnit.CreateUnit(pos.X, pos.Y, type);
                unitsAroundEagle.Add(u);
                u.AddToScene();
            }
        }

        private void RemoveUnitAroundEagle()
        {
            foreach (Unit unit in unitsAroundEagle)
                unit.Dispose();
        }

        private void GetUnitsAroundEagle()
        {
            Rectangle rec = new Rectangle(0, 0, size.Width, size.Height);
            for (int i = 0; i < Scene.Units.Count; i++)
            {
                foreach (Point pos in position)
                {
                    rec.Location = pos;
                    if (rec.IntersectsWith(Scene.Units[i].BoundingBox))
                        unitsAroundEagle.Add(Scene.Units[i]);
                }
            }
        }
    }
}
