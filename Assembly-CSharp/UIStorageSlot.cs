using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06000C32 RID: 3122 RVA: 0x000666E7 File Offset: 0x00064AE7
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.storage != null)) ? null : this.storage.GetItem(this.slot);
		}
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00066711 File Offset: 0x00064B11
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.storage != null)) ? item : this.storage.Replace(this.slot, item);
	}

	// Token: 0x04000AB3 RID: 2739
	public UIItemStorage storage;

	// Token: 0x04000AB4 RID: 2740
	public int slot;
}
