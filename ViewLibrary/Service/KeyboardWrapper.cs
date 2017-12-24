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
        private Action<Exception> ExceptionCollbek;
        private IGameService gameService;
        private Owner owner;

        public KeyboardWrapper(IGameService gameService, Owner owner, Action<Exception> exceptionCollbek)
        {
            this.gameService = gameService;
            this.owner = owner;
            this.ExceptionCollbek = exceptionCollbek;
        }

        public void KeyDown(KeysGame key)
        {
            try
            {
                gameService.KeyDown(key, owner);
            }
            catch (Exception ex)
            {
                ExceptionCollbek.Invoke(ex);
            }
        }

        public void KeyUp(KeysGame key)
        {
            try
            {
                gameService.KeyUp(key, owner);
            }
            catch (Exception ex)
            {
                ExceptionCollbek.Invoke(ex);
            }
        }
    }
}
