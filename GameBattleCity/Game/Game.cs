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

        private void InitGame(IPAddress ipFirstPlayer, IPAddress ipSecondPlayer)
        {
            IKeyboard keyboard = OpenIPlayerHost();

            IRender render;
            ISoundGame sound;
            IGameInfo gameInfo;
            OpenChanelFactoryIPlayer(out render, out sound, out gameInfo);

            if(ipSecondPlayer != null)
            {
                IRender renderIIPlayer;
                ISoundGame soundIIPlayer;
                IGameInfo gameInfoIIPlayer;

                IKeyboard keyboardIIPlayer = OpenIIPlayerHost(ipFirstPlayer.ToString());

                OpenChanelFactoryIIPlayer(out renderIIPlayer, out soundIIPlayer, out gameInfoIIPlayer, ipSecondPlayer.ToString());

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
            ChannelFactory<IRender> factoryRender = new ChannelFactory<IRender>(new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portChannelRender + "/IRender");
            factoryRender.Open();
            renderIIPlayer = factoryRender.CreateChannel();
            ChannelFactory<ISoundGame> factorySound = new ChannelFactory<ISoundGame>(new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portChannelSound + "/ISoundGame");
            factorySound.Open();
            soundIIPlayer = factorySound.CreateChannel();
            ChannelFactory<IGameInfo> factoryGameInfo = new ChannelFactory<IGameInfo>(new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portChannelInfo + "/IGameInfo");
            factoryGameInfo.Open();
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
            hostKeyboardIIPlayer.AddServiceEndpoint(typeof(IKeyboard), new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portHostKeyboard + "/IKeyboard");
            hostKeyboardIIPlayer.Open();

            closeHost += () => hostKeyboardIIPlayer.Close();

            return keyboard;
        }
    }
}
