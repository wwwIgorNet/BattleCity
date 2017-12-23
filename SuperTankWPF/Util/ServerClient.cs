using GameLibrary.Lib;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using SuperTankWPF.Model;
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
    class ServerClient
    {
        private ISoundGame soundGame;
        private IGameInfo gameInfo;
        private IRender render;

        public ServerClient(ISoundGame soundGame, IGameInfo gameInfo, IRender render)
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

        protected double CloseTimeout { get; } = ConfigurationWPF.CloseTimeout;
        public double OpenTimeout { get; } = ConfigurationWPF.OpenTimeout;

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
            FactoryKeyboard.Open(TimeSpan.FromMilliseconds(OpenTimeout));
            return FactoryKeyboard.CreateChannel();
        }

        public virtual void CloseHost()
        {
            HostSound?.Close();
            HostSceneView?.Close();
            HostGameInfo?.Close();
        }

        public virtual void CloseChannel()
        {
            FactoryKeyboard?.Abort();
        }

        public void Close()
        {
            CloseHost();
            CloseChannel();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
