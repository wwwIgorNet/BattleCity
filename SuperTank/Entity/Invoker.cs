using System;
using System.Collections.Generic;

namespace SuperTank
{
    class Invoker
    {
        private Dictionary<TypeCommand, ICommand> commands = new Dictionary<TypeCommand, ICommand>();

        public void AddCommand(TypeCommand type, ICommand commant)
        {
            commands.Add(type, commant);
        }

        public void Handl(TypeCommand command)
        {
            commands[command].Execute();
        }
    }
}