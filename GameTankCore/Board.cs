using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    public static class Board
    {
        private static readonly ObservableCollection<ObjGame> alloObjGame = new ObservableCollection<ObjGame>();
        private static readonly int width = Configuration.WidthBoard;
        private static readonly int height = Configuration.HeightBoard;

        public static ObservableCollection<ObjGame> AlloObj
        {
            get { return alloObjGame; }
        }

        public static int Height { get { return height; } }
        public static int Widtch { get { return width; } }

        public static void Clear()
        {
            alloObjGame.Clear();
        }

        public static ObjGame Colision(ObjGame obj)
        {
            ObjGame colisionObj = null;
            for(int i = 0; i < alloObjGame.Count; i++)
            {
                if(!obj.Equals(alloObjGame[i]) && obj.BoundingBox.IntersectsWith(alloObjGame[i].BoundingBox))
                {
                    colisionObj = alloObjGame[i];
                    break;
                }
            }
            return colisionObj;
        }

        public static bool ColisionBoard(ObjGame obj)
        {
            if (obj.X < 0)
                return true;
            if (obj.Y < 0)
                return true;
            if (obj.X + obj.Width > width)
                return true;
            if (obj.Y + obj.Height > height)
                return true;

            return false;
        }
    }
}
