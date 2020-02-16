using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame.Malee
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	public abstract class ReorderableArray<T> : ICloneable, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x00053B34 File Offset: 0x00051F34
		public ReorderableArray() : this(0)
		{
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00053B3D File Offset: 0x00051F3D
		public ReorderableArray(int length)
		{
			this.array = new List<T>(length);
		}

		// Token: 0x17000153 RID: 339
		public T this[int index]
		{
			get
			{
				return this.array[index];
			}
			set
			{
				this.array[index] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00053B79 File Offset: 0x00051F79
		public int Length
		{
			get
			{
				return this.array.Count;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00053B86 File Offset: 0x00051F86
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00053B89 File Offset: 0x00051F89
		public int Count
		{
			get
			{
				return this.array.Count;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00053B96 File Offset: 0x00051F96
		public object Clone()
		{
			return new List<T>(this.array);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00053BA3 File Offset: 0x00051FA3
		public void CopyFrom(IEnumerable<T> value)
		{
			this.array.Clear();
			this.array.AddRange(value);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00053BBC File Offset: 0x00051FBC
		public bool Contains(T value)
		{
			return this.array.Contains(value);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00053BCA File Offset: 0x00051FCA
		public int IndexOf(T value)
		{
			return this.array.IndexOf(value);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00053BD8 File Offset: 0x00051FD8
		public void Insert(int index, T item)
		{
			this.array.Insert(index, item);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00053BE7 File Offset: 0x00051FE7
		public void RemoveAt(int index)
		{
			this.array.RemoveAt(index);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00053BF5 File Offset: 0x00051FF5
		public void Add(T item)
		{
			this.array.Add(item);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00053C03 File Offset: 0x00052003
		public void Clear()
		{
			this.array.Clear();
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00053C10 File Offset: 0x00052010
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.array.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00053C1F File Offset: 0x0005201F
		public bool Remove(T item)
		{
			return this.array.Remove(item);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00053C2D File Offset: 0x0005202D
		public T[] ToArray()
		{
			return this.array.ToArray();
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00053C3A File Offset: 0x0005203A
		public IEnumerator<T> GetEnumerator()
		{
			return this.array.GetEnumerator();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00053C4C File Offset: 0x0005204C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.array.GetEnumerator();
		}

		// Token: 0x040007D2 RID: 2002
		[SerializeField]
		private List<T> array = new List<T>();
	}
}
