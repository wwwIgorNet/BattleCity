﻿using SuperTank.Command;
using System;
using System.Collections.Generic;

namespace SuperTank
{
    public class Invoker
    {
        private Dictionary<TypeCommand, BaseCommand> commands = new Dictionary<TypeCommand, BaseCommand>();

        public void Add(TypeCommand type, BaseCommand commant)
        {
            commands.Add(type, commant);
        }

        public void Execute(TypeCommand command)
        {
            //BaseCommand a;
            //commands.TryGetValue(command, out a);
            //if(a != null)
            //    a.Execute();
            commands[command].Execute();
        }
    }
}