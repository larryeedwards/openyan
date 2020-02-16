using System;
using UnityEngine;

// Token: 0x0200050B RID: 1291
public class SimpleDetectClickScript : MonoBehaviour
{
	// Token: 0x0600200D RID: 8205 RVA: 0x0014D4A0 File Offset: 0x0014B8A0
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 100f) && raycastHit.collider == this.MyCollider)
			{
				this.Clicked = true;
			}
		}
	}

	// Token: 0x04002CA0 RID: 11424
	public InventoryItemScript InventoryItem;

	// Token: 0x04002CA1 RID: 11425
	public Collider MyCollider;

	// Token: 0x04002CA2 RID: 11426
	public bool Clicked;
}
