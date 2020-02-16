using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000186 RID: 390
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000C2D RID: 3117 RVA: 0x000664D4 File Offset: 0x000648D4
	public List<InvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00066503 File Offset: 0x00064903
	public InvGameItem GetItem(int slot)
	{
		return (slot >= this.items.Count) ? null : this.mItems[slot];
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00066528 File Offset: 0x00064928
	public InvGameItem Replace(int slot, InvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			InvGameItem result = this.items[slot];
			this.mItems[slot] = item;
			return result;
		}
		return item;
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00066560 File Offset: 0x00064960
	private void Start()
	{
		if (this.template != null)
		{
			int num = 0;
			Bounds bounds = default(Bounds);
			for (int i = 0; i < this.maxRows; i++)
			{
				for (int j = 0; j < this.maxColumns; j++)
				{
					GameObject gameObject = base.gameObject.AddChild(this.template);
					Transform transform = gameObject.transform;
					transform.localPosition = new Vector3((float)this.padding + ((float)j + 0.5f) * (float)this.spacing, (float)(-(float)this.padding) - ((float)i + 0.5f) * (float)this.spacing, 0f);
					UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
					if (component != null)
					{
						component.storage = this;
						component.slot = num;
					}
					bounds.Encapsulate(new Vector3((float)this.padding * 2f + (float)((j + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((i + 1) * this.spacing), 0f));
					if (++num >= this.maxItemCount)
					{
						if (this.background != null)
						{
							this.background.transform.localScale = bounds.size;
						}
						return;
					}
				}
			}
			if (this.background != null)
			{
				this.background.transform.localScale = bounds.size;
			}
		}
	}

	// Token: 0x04000AAB RID: 2731
	public int maxItemCount = 8;

	// Token: 0x04000AAC RID: 2732
	public int maxRows = 4;

	// Token: 0x04000AAD RID: 2733
	public int maxColumns = 4;

	// Token: 0x04000AAE RID: 2734
	public GameObject template;

	// Token: 0x04000AAF RID: 2735
	public UIWidget background;

	// Token: 0x04000AB0 RID: 2736
	public int spacing = 128;

	// Token: 0x04000AB1 RID: 2737
	public int padding = 10;

	// Token: 0x04000AB2 RID: 2738
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
