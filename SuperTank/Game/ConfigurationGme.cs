using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class ConfigurationGme : ConfigurationBase
    {
        private static int countLevel = 1;
        public static string Maps { get { return @"Content\Maps\"; } }
        public static int CountLevel { get { return countLevel; } }
    }
}
