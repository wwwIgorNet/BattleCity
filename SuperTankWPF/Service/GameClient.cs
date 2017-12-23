using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTankWPF.Service
{
    public class GameClient : IGameClient
    {
        private IGameInfo gameInfo;
        private IRender render;
        private ISoundGame soundGame;

        public GameClient(IGameInfo gameInfo, IRender render, ISoundGame soundGame)
        {
            this.gameInfo = gameInfo;
            this.render = render;
            this.soundGame = soundGame;
        }

        public void StartGame()
        {
            gameInfo.StartGame();
        }

        public void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            gameInfo.EndLevel(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);
        }

        public void StartLevel(int level)
        {
            gameInfo.StartLevel(level);
        }

        public void GameOver()
        {
            gameInfo.GameOver();
        }

        public void SetCountTankEnemy(int count)
        {
            gameInfo.SetCountTankEnemy(count);
        }

        public void SetCountTankPlaeyr(int count, Owner owner)
        {
            gameInfo.SetCountTankPlaeyr(count, owner);
        }



        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            render.Add(id, typeUnit, x, y, properties);
        }

        public void Remove(int id)
        {
            render.Remove(id);
        }

        public void Clear()
        {
            render.Clear();
        }
        
        public void Update(int id, PropertiesType prop, object value)
        {
            render.Update(id, prop, value);
        }
        
        public void AddRange(List<UnitDataForView> collection)
        {
            render.AddRange(collection);
        }


        public void DetonationTank()
        {
            soundGame.DetonationTank();
        }

        public void DetonationEagle()
        {
            soundGame.DetonationEagle();
        }

        public void DetonationShell()
        {
            soundGame.DetonationShell();
        }

        public void Fire()
        {
            soundGame.Fire();
        }

        public void TwoFire()
        {
            soundGame.TwoFire();
        }

        public void Glide()
        {
            soundGame.Glide();
        }

        public void Move()
        {
            soundGame.Move();
        }

        public void Stop()
        {
            soundGame.Stop();
        }

        public void DetonationBrickWall()
        {
            soundGame.DetonationBrickWall();
        }

        public void TankSoundStop()
        {
            soundGame.TankSoundStop();
        }

        public void Bonus()
        {
            soundGame.Bonus();
        }

        public void NewBonus()
        {
            soundGame.NewBonus();
        }
    }
}
