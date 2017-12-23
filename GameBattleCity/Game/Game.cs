using GameBattleCity.Game;
using GameBattleCity.TwoPlayers;
using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Drawing;
using System.Net;
using System.ServiceModel;

namespace SuperTank
{
    /// <summary>
    /// Initializes the game and manages connections
    /// </summary>
    public class Game
    {
        private ServerGame serverGame = new ServerGame();
        private LevelManager levelManager;
        private Player IPlaeyr;
        private Player IIPlaeyr;
        private Enemy enemy;

        public void Start(IPAddress ipFirstPlayer, IPAddress ipSecondPlayer)
        {
            InitGame(ipFirstPlayer, ipSecondPlayer);

            levelManager.StartLevel();
        }
        public void Start(char[,] map, IPAddress ipFirstPlayer, IPAddress ipSecondPlayer)
        {
            InitGame(ipFirstPlayer, ipSecondPlayer);

            levelManager.StartLevel(map);
        }
        public void CloseChannelFactory()
        {
            serverGame.CloseChannel();
        }
        public void CloseHost()
        {
            serverGame.CloseHost();
        }
        public void Stop()
        {
            levelManager.Stop();
        }

        private void InitGame(IPAddress ipFirstPlayer, IPAddress ipSecondPlayer)
        {
            IKeyboard keyboard = serverGame.OpenIPlayerHost();

            IRender render;
            ISoundGame sound;
            IGameInfo gameInfo;
            serverGame.OpenChanelFactoryIPlayer(out render, out sound, out gameInfo);

            if (ipSecondPlayer != null)
            {
                IRender renderIIPlayer;
                ISoundGame soundIIPlayer;
                IGameInfo gameInfoIIPlayer;

                IKeyboard keyboardIIPlayer = serverGame.OpenIIPlayerHost(ipFirstPlayer.ToString());

                serverGame.OpenChanelFactoryIIPlayer(out renderIIPlayer, out soundIIPlayer, out gameInfoIIPlayer, ipSecondPlayer.ToString());

                render = new RenderTwoPlayers(render, renderIIPlayer);
                gameInfo = new GameInfoTwoPlayers(gameInfo, gameInfoIIPlayer);

                IIPlaeyr = new Player(soundIIPlayer, Owner.IIPlayer, keyboardIIPlayer, gameInfo, () => new Point(ConfigurationGame.StartPositionTankIIPlaeyr.X, ConfigurationGame.StartPositionTankIIPlaeyr.Y));
            }

            IPlaeyr = new Player(sound, Owner.IPlayer, keyboard, gameInfo, () => new Point(ConfigurationGame.StartPositionTankIPlaeyr.X, ConfigurationGame.StartPositionTankIPlaeyr.Y));
            enemy = new Enemy(gameInfo);
            Scene.Render = render;
            levelManager = new LevelManager(sound, gameInfo, IPlaeyr, IIPlaeyr, enemy, keyboard);
        }
    }
}
