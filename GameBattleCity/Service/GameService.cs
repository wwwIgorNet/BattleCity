using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;

namespace GameBattleCity.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameService : IGameService
    {
        private bool isClientConected;
        private OperationContext IPlayerContext;
        private OperationContext IIPlayerContext;

        private IKeyboard keyboardIPlayer;
        private IKeyboard keyboardIIPlayer;

        public GameService(IKeyboard keyboardIPlayer, IKeyboard keyboardIIPlayer)
        {
            ClientsConected = () =>
            {
                isClientConected = true;
            };

            this.keyboardIPlayer = keyboardIPlayer;
            if (keyboardIIPlayer != null)
            {
                this.keyboardIIPlayer = keyboardIIPlayer;
                IsTwoPlayer = true;
            }
        }

        public event Action ClientsConected;
        public event Action<bool> Pause = isPause => { };

        public OperationContext GetClientContext(Owner owner)
        {
            if (owner == Owner.IPlayer)
                return IPlayerContext;
            else if (owner == Owner.IIPlayer)
                return IIPlayerContext;

            return null;
        }

        public bool IsTwoPlayer { get; private set; }

        public void Connect(Owner owner)
        {
            MessageBox.Show("conect client "+owner+" "+isClientConected);

            if (isClientConected) return;

            switch (owner)
            {
                case Owner.IPlayer:
                    IPlayerContext = OperationContext.Current;
                    if (!IsTwoPlayer) ClientsConected.Invoke();
                    else if (IIPlayerContext != null) ClientsConected.Invoke();
                    break;
                case Owner.IIPlayer:
                    IIPlayerContext = OperationContext.Current;
                    if (IPlayerContext != null) ClientsConected.Invoke();
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

        public void PauseGame(bool isPause)
        {
            Pause.Invoke(isPause);
        }
    }
}
