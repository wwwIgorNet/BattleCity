using SuperTank.Audio;
using SuperTank.Updatable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class LevelManager
    {
        private readonly IScene scene;
        private readonly IFactoryUnit factoryUnit;
        private readonly IPlaeyr plaeyr;
        private readonly ISoundGame soundGame;

        public LevelManager(IScene scene, IFactoryUnit factoryUnit, IPlaeyr plaeyr, ISoundGame soundGame)
        {
            this.scene = scene;
            this.factoryUnit = factoryUnit;
            this.plaeyr = plaeyr;
            this.soundGame = soundGame;
        }

        public void CreateLevel(int level)
        {
            scene.Clear();
            List<Unit> objGame = new List<Unit>();
            string[] linesTileMap = File.ReadAllLines(ConfigurationGame.Maps + level);
            int x = 0, y = 0;
            foreach (string line in linesTileMap)
            {
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '#':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.BrickWall));
                            break;
                        case '@':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.ConcreteWall));
                            break;
                        case '~':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.Water));
                            break;
                        case '%':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.Forest));
                            break;
                        case '-':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.Ice));
                            break;
                    }
                    x += ConfigurationGame.WidthTile;
                }
                x = 0;
                y += ConfigurationGame.HeightTile;
            }

            objGame.Add(factoryUnit.Create(12 * ConfigurationGame.WidthTile, ConfigurationGame.HeightBoard - ConfigurationGame.HeightTile * 2, TypeUnit.Eagle));

            scene.AddRange(objGame);

            plaeyr.Unit = factoryUnit.Create(9 * ConfigurationGame.WidthTile,
                ConfigurationGame.HeightBoard - ConfigurationGame.HeigthTank, TypeUnit.PainTank);
            AddPlayerTank();
        }

        public void AddPlayerTank()
        {
            Game.Updatable.Remove(plaeyr);
            Unit star = factoryUnit.Create(plaeyr.Unit.X, plaeyr.Unit.Y, TypeUnit.Star);
            scene.Add(star);
            plaeyr.Unit.PropertyChanged += Unit_PropertyChanged;
            Game.Updatable.Add(new StarUpdate(star, plaeyr, scene));
        }

        private void Unit_PropertyChanged(int id, PropertiesType propType, object objVal)
        {
            switch (propType)
            {
                case PropertiesType.IsStop:
                    if ((bool)objVal) soundGame.Stop();
                    else soundGame.Move();
                    break;
                case PropertiesType.Detonation:
                    break;
                case PropertiesType.Glide:
                    if ((bool)objVal) soundGame.Glide();
                    break;
                case PropertiesType.Fire:
                    if ((bool)objVal) soundGame.Fire();
                    //else soundGame.DetonationShell();
                    break;
            }
        }
    }
}
