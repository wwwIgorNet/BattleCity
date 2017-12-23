using SuperTank.View;
using System.Collections.Generic;
using SuperTank;
using GameLibrary.Service;
using System.Threading;

namespace GameBattleCity.Service
{
    class RenderTwoPlayers : IRender
    {
        private IGameClient IPlayer;
        private IGameClient IIPlayer;

        public RenderTwoPlayers(IGameClient IPlayer, IGameClient IIPlayer)
        {
            this.IPlayer = IPlayer;
            this.IIPlayer = IIPlayer;
        }

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.Add(id, typeUnit, x, y, properties);
                IIPlayer.Add(id, typeUnit, x, y, properties);
            });
        }

        public void AddRange(List<UnitDataForView> collection)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.AddRange(collection);
                IIPlayer.AddRange(collection);
            });
        }

        public void Clear()
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.Clear();
                IIPlayer.Clear();
            });
        }

        public void Remove(int id)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.Remove(id);
                IIPlayer.Remove(id);
            });
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.Update(id, prop, value);
                IIPlayer.Update(id, prop, value);
            });
        }
    }
}
