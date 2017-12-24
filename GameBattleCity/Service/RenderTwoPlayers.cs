using SuperTank.View;
using System.Collections.Generic;
using SuperTank;
using GameLibrary.Service;
using System.Threading;
using System.ServiceModel;
using System;
using static GameBattleCity.Service.ExtendThreadPool;

namespace GameBattleCity.Service
{
    class RenderTwoPlayers : IRender
    {
        private IContextChannel IIPlayerContextChannel;
        private IGameClient IPlayer;
        private IGameClient IIPlayer;

        public RenderTwoPlayers(OperationContext IPlayerContext, OperationContext IIPlayerContext)
        {
            this.IPlayer = IPlayerContext.GetCallbackChannel<IGameClient>();
            this.IIPlayerContextChannel = IIPlayerContext.Channel;
            this.IIPlayer = IIPlayerContext.GetCallbackChannel<IGameClient>();
        }

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.Add(id, typeUnit, x, y, properties);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.Add(id, typeUnit, x, y, properties);
            });
        }

        public void AddRange(List<UnitDataForView> collection)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.AddRange(collection);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.AddRange(collection);
            });
        }

        public void Clear()
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.Clear();
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.Clear();
            });
        }

        public void Remove(int id)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.Remove(id);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.Remove(id);
            });
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.Update(id, prop, value);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.Update(id, prop, value);
            });
        }
    }
}
