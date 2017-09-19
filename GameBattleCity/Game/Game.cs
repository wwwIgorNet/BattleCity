using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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
            Keyboard keyboard = new Keyboard();
            hostKeyboard = new ServiceHost(keyboard);
            hostKeyboard.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostKeyboard.AddServiceEndpoint(typeof(IKeyboard), new NetTcpBinding(), "net.tcp://localhost:9090/IKeyboard");
            hostKeyboard.Open();

            factoryRender = new ChannelFactory<IRender>(new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
            IRender render = factoryRender.CreateChannel();

            factorySound = new ChannelFactory<ISoundGame>(new NetTcpBinding(), "net.tcp://localhost:9090/ISoundGame");
            ISoundGame sound = factorySound.CreateChannel();

            factoryGameInfo = new ChannelFactory<IGameInfo>(new NetTcpBinding(), "net.tcp://localhost:9090/IGameInfo");
            IGameInfo gameInfo = factoryGameInfo.CreateChannel();

            plaeyr = new Player(sound, Owner.Player, keyboard, gameInfo);
            enemy = new Enemy(gameInfo);
            Scene.Render = render;
            levelManager = new LevelManager(sound, gameInfo, plaeyr, enemy);

            render.Init();

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
    }
}
