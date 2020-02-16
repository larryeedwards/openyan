using System;
using UnityEngine;

// Token: 0x02000184 RID: 388
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00066444 File Offset: 0x00064844
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.equipment != null)) ? null : this.equipment.GetItem(this.slot);
		}
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x0006646E File Offset: 0x0006486E
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.equipment != null)) ? item : this.equipment.Replace(this.slot, item);
	}

	// Token: 0x04000AA0 RID: 2720
	public InvEquipment equipment;

	// Token: 0x04000AA1 RID: 2721
	public InvBaseItem.Slot slot;
}
