using GameLibrary.Lib;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace GameBattleCity.Game
{
    class ServerGame : ServerBase
    {
        public void OpenChanelFactoryIIPlayer(out IRender renderIIPlayer, out ISoundGame soundIIPlayer, out IGameInfo gameInfoIIPlayer, string ipAddress)
        {
            ChannelFactory<IRender> factoryRender = new ChannelFactory<IRender>(new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + PortRender + "/IRender");
            factoryRender.Open(TimeSpan.FromMilliseconds(OpenTimeout));
            renderIIPlayer = factoryRender.CreateChannel();
            ChannelFactory<ISoundGame> factorySound = new ChannelFactory<ISoundGame>(new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + PortSound + "/ISoundGame");
            factorySound.Open(TimeSpan.FromMilliseconds(OpenTimeout));
            soundIIPlayer = factorySound.CreateChannel();
            ChannelFactory<IGameInfo> factoryGameInfo = new ChannelFactory<IGameInfo>(new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + PortInfo + "/IGameInfo");
            factoryGameInfo.Open(TimeSpan.FromMilliseconds(OpenTimeout));
            gameInfoIIPlayer = factoryGameInfo.CreateChannel();

            closeChannel += () =>
            {
                factorySound.Close();
                factoryRender.Close();
                factoryGameInfo.Close();
            };
        }
        public void OpenChanelFactoryIPlayer(out IRender render, out ISoundGame sound, out IGameInfo gameInfo)
        {
            ChannelFactory<IRender> factoryRender = new ChannelFactory<IRender>(new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
            factoryRender.Open(TimeSpan.FromMilliseconds(OpenTimeout));
            render = factoryRender.CreateChannel();
            ChannelFactory<ISoundGame> factorySound = new ChannelFactory<ISoundGame>(new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
            factorySound.Open(TimeSpan.FromMilliseconds(OpenTimeout));
            sound = factorySound.CreateChannel();
            ChannelFactory<IGameInfo> factoryGameInfo = new ChannelFactory<IGameInfo>(new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
            factoryGameInfo.Open(TimeSpan.FromMilliseconds(OpenTimeout));
            gameInfo = factoryGameInfo.CreateChannel();

            closeChannel += () =>
            {
                factorySound.Close();
                factoryRender.Close();
                factoryGameInfo.Close();
            };
        }
        public IKeyboard OpenIPlayerHost()
        {
            Keyboard keyboard = new Keyboard();
            ServiceHost hostKeyboardIPlayer = new ServiceHost(keyboard)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(CloseTimeout)
            };
            hostKeyboardIPlayer.AddServiceEndpoint(typeof(IKeyboard), new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
            hostKeyboardIPlayer.Open();

            closeHost += () => hostKeyboardIPlayer.Close();

            return keyboard;
        }
        public IKeyboard OpenIIPlayerHost(string ipAddress)
        {
            Keyboard keyboard = new Keyboard();
            ServiceHost hostKeyboardIIPlayer = new ServiceHost(keyboard)
            {
                CloseTimeout = TimeSpan.FromMilliseconds(CloseTimeout)
            };
            hostKeyboardIIPlayer.AddServiceEndpoint(typeof(IKeyboard), new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + PortKeyboard + "/IKeyboard");
            hostKeyboardIIPlayer.Open();

            closeHost += () => hostKeyboardIIPlayer.Close();

            return keyboard;
        }
    }
}
