using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    public static class Board
    {
        private static List<ObjGame> alloObjGame = new List<ObjGame>();
        private static readonly int width = SettingsGame.WidthBoard;
        private static readonly int height = SettingsGame.HeightBoard;

        public static List<ObjGame> AlloObj
        {
            get { return alloObjGame; }
        }

        public static int Height { get { return height; } }
        public static int Widtch { get { return width; } }

        public static void Clear()
        {
            alloObjGame.Clear();
        }
    }
}
