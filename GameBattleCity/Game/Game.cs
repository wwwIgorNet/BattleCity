using GameBattleCity.Service;
using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Drawing;
using System.Net;
using System.ServiceModel;
using System.Threading;

namespace SuperTank
{
    /// <summary>
    /// Initializes the game and manages connections
    /// </summary>
    public class Game : IDisposable
    {
        private ServiceHost host;
        private GameService gameService;

        private OperationContext operationContextIIPlayer;
        private OperationContext operationContextIPlayer;
        private Keyboard keyboardIPlayer;
        private Keyboard keyboardIIPlayer;
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
            gameService.Pause += Pause;
            gameService.ClientsConected += GameService_ClientConected;

            host = new ServiceHost(gameService)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(ConfigurationGame.CloseTimeout)
            };

            host.AddServiceEndpoint(typeof(IGameService), new NetNamedPipeBinding() { MaxConnections = 1 },
                "net.pipe://localhost/GameService");

            if (ipAddress != null)
                host.AddServiceEndpoint(typeof(IGameService), new NetTcpBinding() { MaxConnections = 1 },
                    "net.tcp://" + ipAddress + ":" + ConfigurationGame.ServisePort + "/GameService");

            host.Open();
        }

        private void GameService_ClientConected()
        {
            operationContextIPlayer = gameService.GetClientContext(Owner.IPlayer);
            IRender render;
            IGameInfo gameInfo;
            ISoundGame soundIPlayer = new SoundForServices(operationContextIPlayer);
            ISoundGame soundIIPlayer = null;
            Player IIPlaeyr = null;

            if (!gameService.IsTwoPlayer)
            {
                render = new Render(operationContextIPlayer);
                gameInfo = new GameInfo(operationContextIPlayer);
            }
            else
            {
                operationContextIIPlayer = gameService.GetClientContext(Owner.IIPlayer);
                soundIIPlayer = new SoundForServices(operationContextIIPlayer);
                render = new RenderTwoPlayers(operationContextIPlayer, operationContextIIPlayer);
                gameInfo = new GameInfoTwoPlayers(operationContextIPlayer, operationContextIIPlayer);

                IIPlaeyr = new Player(soundIIPlayer, Owner.IIPlayer, keyboardIIPlayer, gameInfo,
                    () => new Point(ConfigurationGame.StartPositionTankIIPlaeyr.X, ConfigurationGame.StartPositionTankIIPlaeyr.Y));
            }

            Player IPlaeyr = new Player(soundIPlayer, Owner.IPlayer, keyboardIPlayer, gameInfo, () => new Point(ConfigurationGame.StartPositionTankIPlaeyr.X, ConfigurationGame.StartPositionTankIPlaeyr.Y));
            enemy = new Enemy(gameInfo);
            Scene.Render = render;
            levelManager = new LevelManager(soundIPlayer, gameInfo, IPlaeyr, IIPlaeyr, enemy);

            gameInfo.StartGame();
            if (map == null)
                levelManager.StartLevel();
            else
                levelManager.StartLevel(map);
        }

        public void Pause(bool isPause)
        {
            if (levelManager.Pause(isPause))
            {
                operationContextIPlayer.GetCallbackChannel<IGameClient>().PauseGame(isPause);
                if (operationContextIIPlayer != null && operationContextIIPlayer.Channel.State == CommunicationState.Opened)
                    operationContextIIPlayer?.GetCallbackChannel<IGameClient>().PauseGame(isPause);
            }
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
