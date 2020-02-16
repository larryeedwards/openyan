using System;
using UnityEngine;

namespace MaidDereMinigame.Malee
{
	// Token: 0x0200013C RID: 316
	public class ReorderableAttribute : PropertyAttribute
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x00053AB5 File Offset: 0x00051EB5
		public ReorderableAttribute() : this(null)
		{
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00053ABE File Offset: 0x00051EBE
		public ReorderableAttribute(string elementNameProperty) : this(true, true, true, elementNameProperty, null, null)
		{
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00053ACC File Offset: 0x00051ECC
		public ReorderableAttribute(string elementNameProperty, string elementIconPath) : this(true, true, true, elementNameProperty, null, elementIconPath)
		{
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00053ADA File Offset: 0x00051EDA
		public ReorderableAttribute(string elementNameProperty, string elementNameOverride, string elementIconPath) : this(true, true, true, elementNameProperty, elementNameOverride, elementIconPath)
		{
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00053AE8 File Offset: 0x00051EE8
		public ReorderableAttribute(bool add, bool remove, bool draggable, string elementNameProperty = null, string elementIconPath = null) : this(add, remove, draggable, elementNameProperty, null, elementIconPath)
		{
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00053AF8 File Offset: 0x00051EF8
		public ReorderableAttribute(bool add, bool remove, bool draggable, string elementNameProperty = null, string elementNameOverride = null, string elementIconPath = null)
		{
			this.add = add;
			this.remove = remove;
			this.draggable = draggable;
			this.sortable = true;
			this.elementNameProperty = elementNameProperty;
			this.elementNameOverride = elementNameOverride;
			this.elementIconPath = elementIconPath;
		}

		// Token: 0x040007C8 RID: 1992
		public bool add;

		// Token: 0x040007C9 RID: 1993
		public bool remove;

		// Token: 0x040007CA RID: 1994
		public bool draggable;

		// Token: 0x040007CB RID: 1995
		public bool singleLine;

		// Token: 0x040007CC RID: 1996
		public bool paginate;

		// Token: 0x040007CD RID: 1997
		public bool sortable;

		// Token: 0x040007CE RID: 1998
		public int pageSize;

		// Token: 0x040007CF RID: 1999
		public string elementNameProperty;

		// Token: 0x040007D0 RID: 2000
		public string elementNameOverride;

		// Token: 0x040007D1 RID: 2001
		public string elementIconPath;
	}
}
