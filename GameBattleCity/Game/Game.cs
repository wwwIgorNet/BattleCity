using GameBattleCity.Game;
using GameBattleCity.Service;
using GameLibrary.Lib;
using GameLibrary.Service;
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
    public class Game : IDisposable
    {
        private ServiceHost host;
        private GameService gameService;

        Keyboard keyboardIPlayer;
        Keyboard keyboardIIPlayer;
        private LevelManager levelManager;
        private Enemy enemy;
        private char[,] map;

        public void Start(IPAddress ipAddress)
        {
            Start(null, ipAddress);
        }
        public void Start(char[,] map, IPAddress ipAddress)
        {
            this.map = map;
            InitGame(ipAddress);
        }

        private void InitGame(IPAddress ipAddress)
        {
            keyboardIPlayer = new Keyboard();

            if (ipAddress != null)
                keyboardIIPlayer = new Keyboard();

            gameService = new GameService(keyboardIPlayer, keyboardIIPlayer);
            gameService.ClientsConected += GameService_ClientConected;

            host = new ServiceHost(gameService);
            host.CloseTimeout = TimeSpan.FromMilliseconds(ConfigurationGame.CloseTimeout);
            host.AddServiceEndpoint(typeof(IGameService), new NetNamedPipeBinding(),
                "net.pipe://localhost/GameService");

            if (ipAddress != null)
                host.AddServiceEndpoint(typeof(IGameService), new NetTcpBinding(),
                    "net.tcp://" + ipAddress + ":9090/GameService");

                host.Open();
        }

        private void GameService_ClientConected()
        {
            IRender render;
            IGameInfo gameInfo;
            ISoundGame soundIPlayer = new SoundForServices(gameService.IPlayerClient);
            ISoundGame soundIIPlayer = null;
            Player IIPlaeyr = null;

            if (!gameService.IsTwoPlayer)
            {
                render = new Render(gameService.IPlayerClient);
                gameInfo = new GameInfo(gameService.IPlayerClient);
            }
            else
            {
                soundIIPlayer = new SoundForServices(gameService.IIPlayerClient);
                render = new RenderTwoPlayers(gameService.IPlayerClient, gameService.IIPlayerClient);
                gameInfo = new GameInfoTwoPlayers(gameService.IPlayerClient, gameService.IIPlayerClient);

                IIPlaeyr = new Player(soundIIPlayer, Owner.IIPlayer, keyboardIIPlayer, gameInfo,
                    () => new Point(ConfigurationGame.StartPositionTankIIPlaeyr.X, ConfigurationGame.StartPositionTankIIPlaeyr.Y));
            }

            Player IPlaeyr = new Player(soundIPlayer, Owner.IPlayer, keyboardIPlayer, gameInfo, () => new Point(ConfigurationGame.StartPositionTankIPlaeyr.X, ConfigurationGame.StartPositionTankIPlaeyr.Y));
            enemy = new Enemy(gameInfo);
            Scene.Render = render;
            levelManager = new LevelManager(soundIPlayer, gameInfo, IPlaeyr, IIPlaeyr, enemy, keyboardIPlayer);

            gameInfo.StartGame();
            if (map == null)
                levelManager.StartLevel();
            else
                levelManager.StartLevel(map);
        }

        public void CloseHost()
        {
            host.Close();
        }
        public void Stop()
        {
            levelManager?.Stop();
        }
        public void Dispose()
        {
            Stop();
            host.Close();
        }
    }
}
