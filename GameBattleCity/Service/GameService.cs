using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace GameBattleCity.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameService : IGameService
    {
        private IKeyboard keyboardIPlayer;
        private IKeyboard keyboardIIPlayer;

        public GameService(IKeyboard keyboardIPlayer, IKeyboard keyboardIIPlayer)
        {
            this.keyboardIPlayer = keyboardIPlayer;
            if (keyboardIIPlayer != null)
            {
                this.keyboardIIPlayer = keyboardIIPlayer;
                IsTwoPlayer = true;
            }
        }

        public event Action ClientsConected = () => { };

        public IGameClient IPlayerClient { get; private set; }
        public IGameClient IIPlayerClient { get; private set; }

        public bool IsTwoPlayer { get; private set; }

        public void Connect(Owner owner)
        {
            switch (owner)
            {
                case Owner.IPlayer:
                    IPlayerClient = OperationContext.Current.GetCallbackChannel<IGameClient>();
                    if (!IsTwoPlayer) ClientsConected.Invoke();
                    else if (IIPlayerClient != null) ClientsConected.Invoke();
                    break;
                case Owner.IIPlayer:
                    IIPlayerClient = OperationContext.Current.GetCallbackChannel<IGameClient>();
                    if (IPlayerClient != null) ClientsConected.Invoke();
                    break;
            }
        }

        public void KeyDown(KeysGame key, Owner owner)
        {
            switch (owner)
            {
                case Owner.IPlayer:
                    keyboardIPlayer.KeyDown(key);
                    break;
                case Owner.IIPlayer:
                    keyboardIIPlayer.KeyDown(key);
                    break;
            }
        }

        public void KeyUp(KeysGame key, Owner owner)
        {
            switch (owner)
            {
                case Owner.IPlayer:
                    keyboardIPlayer.KeyUp(key);
                    break;
                case Owner.IIPlayer:
                    keyboardIIPlayer.KeyUp(key);
                    break;
            }
        }
    }
}
