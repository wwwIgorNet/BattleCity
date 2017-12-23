using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewLibrary.Service
{
    public class KeyboardWrapper : IKeyboard
    {
        private IGameService gameService;
        private Owner owner;

        public KeyboardWrapper(IGameService gameService, Owner owner)
        {
            this.gameService = gameService;
            this.owner = owner;
        }

        public void KeyDown(KeysGame key)
        {
            gameService.KeyDown(key, owner);
        }

        public void KeyUp(KeysGame key)
        {
            gameService.KeyUp(key, owner);
        }
    }
}
