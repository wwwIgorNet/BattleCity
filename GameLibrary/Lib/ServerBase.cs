using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Lib
{
    public abstract class ServerBase : IDisposable
    {
        protected int PortKeyboard { get; } = 9091;
        protected int PortRender { get; } = 9090;
        protected int PortSound { get; } = 9090;
        protected int PortInfo { get; } = 9090;

        protected int CloseTimeout { get; } = ConfigurationBase.CloseTimeout;
        protected int OpenTimeout { get; } = ConfigurationBase.OpenTimeout;

        protected Action closeChannel = () => { };
        protected Action closeHost = () => { };

        public void CloseHost()
        {
            closeHost.Invoke();
            closeHost = () => { };
        }
        public void CloseChannel()
        {
            closeChannel.Invoke();
            closeChannel = () => { };
        }

        public void Dispose()
        {
            CloseHost();
            CloseChannel();
        }
    }
}
