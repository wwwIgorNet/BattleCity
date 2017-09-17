using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SuperTank
{
    public class Game : IDisposable
    {
        private readonly LevelManager levelManager;

        private Player plaeyr;
        private Enemy enemy;

        public Game(IRender render, ISoundGame soundGame, IKeyboard keyboard, IGameInfo gameInfo)
        {
            plaeyr = new Player(soundGame, Owner.Player, keyboard, gameInfo);
            enemy = new Enemy(gameInfo);
            Scene.Render = render;
            levelManager = new LevelManager(soundGame, gameInfo, plaeyr, enemy);

        }

        public void Start()
        {
            levelManager.StartLevel();
        }

        public void Dispose()
        {
            levelManager.Stop();
        }
    }
}
