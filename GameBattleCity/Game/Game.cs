using SuperTank.Audio;
using SuperTank.View;
using System;
using System.ServiceModel;

namespace SuperTank
{
    public class Game
    {
        private LevelManager levelManager;
        private Player plaeyr;
        private Enemy enemy;

        private ServiceHost hostKeyboard;
        private ChannelFactory<IRender> factoryRender;
        private ChannelFactory<ISoundGame> factorySound;
        private ChannelFactory<IGameInfo> factoryGameInfo;

        public void Start()
        {
            InitGame();

            levelManager.StartLevel();
        }
        public void Stop()
        {
            levelManager.Stop();
        }
        public void CloseFactory()
        {
            factorySound.Abort();
            factoryRender.Abort();
            factoryGameInfo.Abort();
        }
        public void CloseHost()
        {
            hostKeyboard.Close();
        }

        private void InitGame()
        {
            Keyboard keyboard = new Keyboard();
            hostKeyboard = new ServiceHost(keyboard);
            hostKeyboard.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostKeyboard.AddServiceEndpoint(typeof(IKeyboard), new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
            hostKeyboard.Open();

            factoryRender = new ChannelFactory<IRender>(new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
            IRender render = factoryRender.CreateChannel();

            factorySound = new ChannelFactory<ISoundGame>(new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
            ISoundGame sound = factorySound.CreateChannel();

            factoryGameInfo = new ChannelFactory<IGameInfo>(new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
            IGameInfo gameInfo = factoryGameInfo.CreateChannel();

            plaeyr = new Player(sound, Owner.Player, keyboard, gameInfo);
            enemy = new Enemy(gameInfo);
            Scene.Render = render;
            levelManager = new LevelManager(sound, gameInfo, plaeyr, enemy);

            render.Init();
        }

        public void Start(char[,] map)
        {
            InitGame();

            levelManager.StartLevel(map);
        }
    }
}
