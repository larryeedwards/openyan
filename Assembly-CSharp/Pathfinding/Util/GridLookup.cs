using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020000BB RID: 187
	public class GridLookup<T> where T : class
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x00037058 File Offset: 0x00035458
		public GridLookup(Int2 size)
		{
			this.size = size;
			this.cells = new GridLookup<T>.Item[size.x * size.y];
			for (int i = 0; i < this.cells.Length; i++)
			{
				this.cells[i] = new GridLookup<T>.Item();
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000370D3 File Offset: 0x000354D3
		public GridLookup<T>.Root AllItems
		{
			get
			{
				return this.all.next;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000370E0 File Offset: 0x000354E0
		public GridLookup<T>.Root GetRoot(T item)
		{
			GridLookup<T>.Root result;
			this.rootLookup.TryGetValue(item, out result);
			return result;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00037100 File Offset: 0x00035500
		public GridLookup<T>.Root Add(T item, IntRect bounds)
		{
			GridLookup<T>.Root root = new GridLookup<T>.Root
			{
				obj = item,
				prev = this.all,
				next = this.all.next
			};
			this.all.next = root;
			if (root.next != null)
			{
				root.next.prev = root;
			}
			this.rootLookup.Add(item, root);
			this.Move(item, bounds);
			return root;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00037174 File Offset: 0x00035574
		public void Remove(T item)
		{
			GridLookup<T>.Root root;
			if (!this.rootLookup.TryGetValue(item, out root))
			{
				return;
			}
			this.Move(item, new IntRect(0, 0, -1, -1));
			this.rootLookup.Remove(item);
			root.prev.next = root.next;
			if (root.next != null)
			{
				root.next.prev = root.prev;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000371E0 File Offset: 0x000355E0
		public void Move(T item, IntRect bounds)
		{
			GridLookup<T>.Root root;
			if (!this.rootLookup.TryGetValue(item, out root))
			{
				throw new ArgumentException("The item has not been added to this object");
			}
			IntRect previousBounds = root.previousBounds;
			if (previousBounds == bounds)
			{
				return;
			}
			for (int i = 0; i < root.items.Count; i++)
			{
				GridLookup<T>.Item item2 = root.items[i];
				item2.prev.next = item2.next;
				if (item2.next != null)
				{
					item2.next.prev = item2.prev;
				}
			}
			root.previousBounds = bounds;
			int num = 0;
			for (int j = bounds.ymin; j <= bounds.ymax; j++)
			{
				for (int k = bounds.xmin; k <= bounds.xmax; k++)
				{
					GridLookup<T>.Item item3;
					if (num < root.items.Count)
					{
						item3 = root.items[num];
					}
					else
					{
						item3 = ((this.itemPool.Count <= 0) ? new GridLookup<T>.Item() : this.itemPool.Pop());
						item3.root = root;
						root.items.Add(item3);
					}
					num++;
					item3.prev = this.cells[k + j * this.size.x];
					item3.next = item3.prev.next;
					item3.prev.next = item3;
					if (item3.next != null)
					{
						item3.next.prev = item3;
					}
				}
			}
			for (int l = root.items.Count - 1; l >= num; l--)
			{
				GridLookup<T>.Item item4 = root.items[l];
				item4.root = null;
				item4.next = null;
				item4.prev = null;
				root.items.RemoveAt(l);
				this.itemPool.Push(item4);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000373EC File Offset: 0x000357EC
		public List<U> QueryRect<U>(IntRect r) where U : class, T
		{
			List<U> list = ListPool<U>.Claim();
			for (int i = r.ymin; i <= r.ymax; i++)
			{
				int num = i * this.size.x;
				for (int j = r.xmin; j <= r.xmax; j++)
				{
					GridLookup<T>.Item item = this.cells[j + num];
					while (item.next != null)
					{
						item = item.next;
						U u = item.root.obj as U;
						if (!item.root.flag && u != null)
						{
							item.root.flag = true;
							list.Add(u);
						}
					}
				}
			}
			for (int k = r.ymin; k <= r.ymax; k++)
			{
				int num2 = k * this.size.x;
				for (int l = r.xmin; l <= r.xmax; l++)
				{
					GridLookup<T>.Item item2 = this.cells[l + num2];
					while (item2.next != null)
					{
						item2 = item2.next;
						item2.root.flag = false;
					}
				}
			}
			return list;
		}

		// Token: 0x04000504 RID: 1284
		private Int2 size;

		// Token: 0x04000505 RID: 1285
		private GridLookup<T>.Item[] cells;

		// Token: 0x04000506 RID: 1286
		private GridLookup<T>.Root all = new GridLookup<T>.Root();

		// Token: 0x04000507 RID: 1287
		private Dictionary<T, GridLookup<T>.Root> rootLookup = new Dictionary<T, GridLookup<T>.Root>();

		// Token: 0x04000508 RID: 1288
		private Stack<GridLookup<T>.Item> itemPool = new Stack<GridLookup<T>.Item>();

		// Token: 0x020000BC RID: 188
		internal class Item
		{
			// Token: 0x04000509 RID: 1289
			public GridLookup<T>.Root root;

			// Token: 0x0400050A RID: 1290
			public GridLookup<T>.Item prev;

			// Token: 0x0400050B RID: 1291
			public GridLookup<T>.Item next;
		}

		// Token: 0x020000BD RID: 189
		public class Root
		{
			// Token: 0x0400050C RID: 1292
			public T obj;

			// Token: 0x0400050D RID: 1293
			public GridLookup<T>.Root next;

			// Token: 0x0400050E RID: 1294
			internal GridLookup<T>.Root prev;

			// Token: 0x0400050F RID: 1295
			internal IntRect previousBounds = new IntRect(0, 0, -1, -1);

			// Token: 0x04000510 RID: 1296
			internal List<GridLookup<T>.Item> items = new List<GridLookup<T>.Item>();

			// Token: 0x04000511 RID: 1297
			internal bool flag;
		}
	}
}
