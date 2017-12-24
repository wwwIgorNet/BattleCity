using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameBattleCity.Service
{
    static class ExtendThreadPool
    {
        public static void InvokWithTryCatch(Action action)
        {
            ThreadPool.QueueUserWorkItem(waitCallback, action);
        }

        private static WaitCallback waitCallback = s =>
        {
            try
            {
                ((Action)s).Invoke();
            }
            catch { }
        };
    }
}
