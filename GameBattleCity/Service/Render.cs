using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace GameBattleCity.Service
{
    public class Render : IRender
    {
        private IGameClient gameClient;

        public Render(OperationContext gameClientContext)
        {
            this.gameClient = gameClientContext.GetCallbackChannel<IGameClient>();
        }

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            gameClient.Add(id, typeUnit, x, y, properties);
        }

        public void AddRange(List<UnitDataForView> collection)
        {
            gameClient.AddRange(collection);
        }

        public void Clear()
        {
            gameClient.Clear();
        }

        public void Remove(int id)
        {
            gameClient.Remove(id);
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            gameClient.Update(id, prop, value);
        }
    }
}
