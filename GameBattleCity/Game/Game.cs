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
        private readonly int portHostKeyboard = 9091;
        private readonly int portChannelRender = 9090;
        private readonly int portChannelSound = 9090;
        private readonly int portChannelInfo = 9090;

        private LevelManager levelManager;
        private Player IPlaeyr;
        private Player IIPlaeyr;
        private Enemy enemy;
        
        private Action closeChannel = () => { };
        private Action closeHost = () => { };

        public void Start(IPAddress ipIIPlayer)
        {
            InitGame(ipIIPlayer);

            levelManager.StartLevel();
        }
        public void Start(char[,] map, IPAddress ipIIPlayer)
        {
            InitGame(ipIIPlayer);

            levelManager.StartLevel(map);
        }
        public void CloseChannelFactory()
        {
            closeChannel.Invoke();
            closeChannel = () => { };
        }
        public void CloseHost()
        {
            closeHost.Invoke();
            closeHost = () => { };
        }
        public void Stop()
        {
            levelManager.Stop();
        }

        private void InitGame(IPAddress ipIIPlayer)
        {
            IKeyboard keyboard = OpenIPlayerHost();

            IRender render;
            ISoundGame sound;
            IGameInfo gameInfo;
            OpenChanelFactoryIPlayer(out render, out sound, out gameInfo);

            if(ipIIPlayer != null)
            {
                IRender renderIIPlayer;
                ISoundGame soundIIPlayer;
                IGameInfo gameInfoIIPlayer;

                IKeyboard keyboardIIPlayer = OpenIIPlayerHost(ipIIPlayer.ToString());

                OpenChanelFactoryIIPlayer(out renderIIPlayer, out soundIIPlayer, out gameInfoIIPlayer, ipIIPlayer.ToString());

                render = new RenderTwoPlayers(render, renderIIPlayer);
                gameInfo = new GameInfoTwoPlayers(gameInfo, gameInfoIIPlayer);

                IIPlaeyr = new Player(soundIIPlayer, Owner.IIPlayer, keyboardIIPlayer, gameInfo, () => new Point(ConfigurationGame.StartPositionTankIIPlaeyr.X, ConfigurationGame.StartPositionTankIIPlaeyr.Y));
            }

            IPlaeyr = new Player(sound, Owner.IPlayer, keyboard, gameInfo, ()=> new Point(ConfigurationGame.StartPositionTankIPlaeyr.X, ConfigurationGame.StartPositionTankIPlaeyr.Y));
            enemy = new Enemy(gameInfo);
            Scene.Render = render;
            levelManager = new LevelManager(sound, gameInfo, IPlaeyr, IIPlaeyr, enemy);
        }
        private void OpenChanelFactoryIIPlayer(out IRender renderIIPlayer, out ISoundGame soundIIPlayer, out IGameInfo gameInfoIIPlayer, string ipAddress)
        {
            ChannelFactory<IRender> factoryRender = new ChannelFactory<IRender>(new NetTcpBinding(), "net.tcp://" + ipAddress + ":" + portChannelRender + "/IRender");
            renderIIPlayer = factoryRender.CreateChannel();
            ChannelFactory<ISoundGame> factorySound = new ChannelFactory<ISoundGame>(new NetTcpBinding(), "net.tcp://" + ipAddress + ":" + portChannelSound + "/ISoundGame");
            soundIIPlayer = factorySound.CreateChannel();
            ChannelFactory<IGameInfo> factoryGameInfo = new ChannelFactory<IGameInfo>(new NetTcpBinding(), "net.tcp://" + ipAddress + ":" + portChannelInfo + "/IGameInfo");
            gameInfoIIPlayer = factoryGameInfo.CreateChannel();

            closeChannel += () =>
            {
                factorySound.Abort();
                factoryRender.Abort();
                factoryGameInfo.Abort();
            };
        }
        private void OpenChanelFactoryIPlayer(out IRender render, out ISoundGame sound, out IGameInfo gameInfo)
        {
            ChannelFactory<IRender> factoryRender = new ChannelFactory<IRender>(new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
            render = factoryRender.CreateChannel();
            ChannelFactory<ISoundGame> factorySound = new ChannelFactory<ISoundGame>(new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
            sound = factorySound.CreateChannel();
            ChannelFactory<IGameInfo> factoryGameInfo = new ChannelFactory<IGameInfo>(new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
            gameInfo = factoryGameInfo.CreateChannel();

            closeChannel += () =>
            {
                factorySound.Abort();
                factoryRender.Abort();
                factoryGameInfo.Abort();
            };
        }
        private IKeyboard OpenIPlayerHost()
        {
            Keyboard keyboard = new Keyboard();
            ServiceHost hostKeyboardIPlayer = new ServiceHost(keyboard);
            hostKeyboardIPlayer.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostKeyboardIPlayer.AddServiceEndpoint(typeof(IKeyboard), new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
            hostKeyboardIPlayer.Open();

            closeHost += () => hostKeyboardIPlayer.Close();

            return keyboard;
        }
        private IKeyboard OpenIIPlayerHost(string ipAddress)
        {
            Keyboard keyboard = new Keyboard();
            ServiceHost hostKeyboardIIPlayer = new ServiceHost(keyboard);
            hostKeyboardIIPlayer.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostKeyboardIIPlayer.AddServiceEndpoint(typeof(IKeyboard), new NetTcpBinding(), "net.tcp://" + ipAddress + ":" + portHostKeyboard + "/IKeyboard");
            hostKeyboardIIPlayer.Open();

            closeHost += () => hostKeyboardIIPlayer.Close();

            return keyboard;
        }
    }
}
