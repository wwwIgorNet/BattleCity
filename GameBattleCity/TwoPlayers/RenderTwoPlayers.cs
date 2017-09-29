using SuperTank.View;
using System.Collections.Generic;
using SuperTank;

namespace GameBattleCity.TwoPlayers
{
    class RenderTwoPlayers : IRender
    {
        private IRender IPlayer;
        private IRender IIPlayer;

        public RenderTwoPlayers(IRender IPlayer, IRender IIPlayer)
        {
            this.IPlayer = IPlayer;
            this.IIPlayer = IIPlayer;
        }

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            IPlayer.Add(id, typeUnit, x, y, properties);
            IIPlayer.Add(id, typeUnit, x, y, properties);
        }

        public void AddRange(List<UnitDataForView> collection)
        {
            IPlayer.AddRange(collection);
            IIPlayer.AddRange(collection);
        }

        public void Clear()
        {
            IPlayer.Clear();
            IIPlayer.Clear();
        }

        public void Init()
        {
            IPlayer.Init();
            IIPlayer.Init();
        }

        public void Remove(int id)
        {
            IPlayer.Remove(id);
            IIPlayer.Remove(id);
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            IPlayer.Update(id, prop, value);
            IIPlayer.Update(id, prop, value);
        }
    }
}
