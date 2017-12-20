using SuperTank;
using SuperTank.Audio;
using SuperTank.Comunication;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.Util
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ComunicationTCP : Comunication, ITwoComputer
    {
        private readonly int portChannelKeyboard = 9091;
        private readonly int portTwoComputer = 9092;
        private readonly int portHostRender = 9090;
        private readonly int portHostSound = 9090;
        private readonly int portHostInfo = 9090;

        private ServiceHost hostTwoComputer;
        private ChannelFactory<ITwoComputer> factoryTwoComputer;

        public ComunicationTCP(ISoundGame soundGame, IGameInfo gameInfo, IRender render)
            : base(soundGame, gameInfo, render)
        { }

        public event Action StartedTwoComputer;

        public IKeyboard GetTCPKeyboard(IPAddress iPRemoteComputer)
        {
            string ipAddress = iPRemoteComputer.ToString();
            FactoryKeyboard = new ChannelFactory<IKeyboard>(new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portChannelKeyboard + "/IKeyboard");
            FactoryKeyboard.Open(TimeSpan.FromSeconds(1));
            return FactoryKeyboard.CreateChannel();
        }

        public void OpenTCPHost(IPAddress iPCurrentComputer)
        {
            string ipAddress = iPCurrentComputer.ToString();
            HostSound = new ServiceHost(SoundGame)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(CloseTimeout)
            };
            HostSound.AddServiceEndpoint(typeof(ISoundGame), new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portHostSound + "/ISoundGame");
            HostSound.Open();

            HostSceneView = new ServiceHost(Render)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(CloseTimeout)
            };
            HostSceneView.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portHostRender + "/IRender");
            HostSceneView.Open();

            HostGameInfo = new ServiceHost(GameInfo)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(CloseTimeout)
            };
            HostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portHostInfo + "/IGameInfo");
            HostGameInfo.Open();
        }

        public void StartMainComputer(IPAddress iPAddressCurrentComputer)
        {
            hostTwoComputer = new ServiceHost(this)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(0)
            };
            string conStr = "net.tcp://" + iPAddressCurrentComputer.ToString() + ":" + portTwoComputer + "/ITwoComputer";
            hostTwoComputer.AddServiceEndpoint(typeof(ITwoComputer), new NetTcpBinding(SecurityMode.None), conStr);
            hostTwoComputer.Open();
        }

        public void StartTwoPlayerComputer(IPAddress iPAddressRemouteComputr)
        {
            string conStr = "net.tcp://" + iPAddressRemouteComputr.ToString() + ":" + portTwoComputer + "/ITwoComputer";
            factoryTwoComputer = new ChannelFactory<ITwoComputer>(new NetTcpBinding(SecurityMode.None), conStr);
            factoryTwoComputer.CreateChannel().StartedTwoComp();
            factoryTwoComputer.Abort();
        }

        void ITwoComputer.StartedTwoComp()
        {
            hostTwoComputer.Close();
            StartedTwoComputer?.Invoke();
        }

        public override void CloseChannelFactory()
        {
            factoryTwoComputer?.Close();
            base.CloseChannelFactory();
        }

        public override void CloseHost()
        {
            StartedTwoComputer = null;
            hostTwoComputer?.Close();
            base.CloseHost();
        }
    }
}
