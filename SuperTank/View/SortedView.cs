using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SuperTank
{
    public class SortedView : IEnumerable<BaseView>
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

        public BaseView FindByID(int id)
        {
            BaseView res = null;
            foreach (var item in drowable)
            {
                 res = item.Value.Find(v => v.ID.Equals(id));
                if (res != null) break;
            }
            return res;
        }

        public void Remove(int id)
        {
            foreach (var item in drowable)
            {
                item.Value.RemoveAll(v => v.ID == id);
            }
        }

        internal void Clear()
        {
            foreach (var item in drowable)
            {
                item.Value.Clear();
            }
        }

        public IEnumerator<BaseView> GetEnumerator()
        {
            for (int i = 0; i < drowable.Keys.Count; i++)
            {
                for (int y = 0; y < drowable.Values[i].Count; y++)
                {
                    yield return drowable.Values[i][y];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
