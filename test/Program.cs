using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle r1 = new Rectangle(0, 0, 20, 20);
            Rectangle r2 = new Rectangle(0, 20, 20, 20);
            Console.WriteLine(r1.IntersectsWith(r2));

            Console.ReadKey();
        }
    }
}
