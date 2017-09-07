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
        private static ISoundGame soundGame;
        private static IGameInfo gameInfo;

        private Player plaeyr;
        private Enemy enemy;

        public Game(IRender render, ISoundGame soundGame, IKeyboard keyboard, IGameInfo gameInfo)
        {
            Game.gameInfo = gameInfo;
            plaeyr = new Player(soundGame, Owner.Player);
            enemy = new Enemy();
            Game.Keyboard = keyboard;
            Game.soundGame = soundGame;
            Scene.Render = render;
            levelManager = new LevelManager(soundGame, gameInfo, plaeyr, enemy);
        }

        public static IKeyboard Keyboard { get; set; }
        public static IGameInfo GameInfo { get { return gameInfo; } }

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
