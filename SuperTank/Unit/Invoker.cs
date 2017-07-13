using SuperTank.Command;
using System;
using System.Collections.Generic;

namespace SuperTank
{
    public class Invoker
    {
        private Dictionary<TypeCommand, Action> commands = new Dictionary<TypeCommand, Action>();

        public void AddCommand(TypeCommand type, Action commant)
        {
            commands.Add(type, commant);
        }

        public void Execute(TypeCommand command)
        {
            commands[command].Invoke();
        }
    }
}