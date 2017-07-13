using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class SortedView
    {
        private readonly SortedList<int, List<BaseView>> drowable = new SortedList<int, List<BaseView>>(new Int32());

        public void Add(BaseView view)
        {
            List<BaseView> list = null;

            if (!drowable.ContainsKey(view.ZIndex))
            {
                list = new List<BaseView>();
                drowable.Add(view.ZIndex, list);
            }
            else
                list = drowable[view.ZIndex];
            list.Add(view);
        }

        public void Remove(Predicate<BaseView> math)
        {
            foreach (var item in drowable)
            {
                item.Value.RemoveAll(math);
            }
        }

        public void DrowAll(Graphics g)
        {
            foreach (var item in drowable)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    item.Value[i].Draw(g);
                }
            }
        }

        internal void Clear()
        {
            foreach (var item in drowable)
            {
                item.Value.Clear();
            }
        }
    }
}
