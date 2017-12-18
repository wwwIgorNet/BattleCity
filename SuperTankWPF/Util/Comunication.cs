using Microsoft.Practices.ServiceLocation;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using SuperTankWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperTankWPF.Util
{
    class Comunication : IDisposable
    {
        private ISoundGame soundGame;
        private IGameInfo gameInfo;
        private IRender render;

        public Comunication(ISoundGame soundGame, IGameInfo gameInfo, IRender render)
        {
            this.soundGame = soundGame;
            this.gameInfo = gameInfo;
            this.render = render;
        }

        protected ChannelFactory<IKeyboard> FactoryKeyboard { get; set; }
        protected ServiceHost HostGameInfo { get; set; }
        protected ServiceHost HostSceneView { get; set; }
        protected ServiceHost HostSound { get; set; }

        protected ISoundGame SoundGame { get => soundGame; }
        protected IGameInfo GameInfo { get => gameInfo; }
        protected IRender Render { get => render; }

        protected int CloseTimeout { get; } = 50;

        public void OpenHost()
        {
            HostSound = new ServiceHost(SoundGame)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(CloseTimeout)
            };
            HostSound.AddServiceEndpoint(typeof(ISoundGame), new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
            HostSound.Open();

            HostGameInfo = new ServiceHost(GameInfo)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(CloseTimeout)
            };
            HostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
            HostGameInfo.Open();

            HostSceneView = new ServiceHost(Render)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(CloseTimeout)
            };
            HostSceneView.AddServiceEndpoint(typeof(IRender), new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
            HostSceneView.Open();

        }
        public IKeyboard GetKeyboard()
        {
            FactoryKeyboard = new ChannelFactory<IKeyboard>(new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
            FactoryKeyboard.Open(TimeSpan.FromSeconds(1));
            return FactoryKeyboard.CreateChannel();
        }

        public void CloseHost()
        {
            HostSound?.Close();
            HostSceneView?.Close();
            HostGameInfo?.Close();
        }

        public void CloseChannelFactory()
        {
            FactoryKeyboard?.Close();
        }

        public void Close()
        {
            CloseHost();
            CloseChannelFactory();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
