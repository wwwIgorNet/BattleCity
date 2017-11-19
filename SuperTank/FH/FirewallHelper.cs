using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SuperTank.FH
{
    class FirewallHelper
    {
        /// <summary>
        /// Запрашивает разришения брендмауера на запуск сервисов для етого приложения
        /// </summary>
        public static void Test()
        {
            try
            {

                ServiceHost hostSound = new ServiceHost(typeof(TestService));
                hostSound.AddServiceEndpoint(typeof(ITestContract), new NetTcpBinding(), "net.tcp://localhost:7777/ITestContract");
                hostSound.Open();

                hostSound.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [ServiceContract]
        interface ITestContract
        {
            [OperationContract]
            void Test();
        }
        class TestService : ITestContract
        {
            public void Test()
            { }
        }
    }
}
