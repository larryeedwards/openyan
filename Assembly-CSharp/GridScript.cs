using System;
using UnityEngine;

// Token: 0x0200040B RID: 1035
public class GridScript : MonoBehaviour
{
	// Token: 0x06001C5E RID: 7262 RVA: 0x000FD660 File Offset: 0x000FBA60
	private void Start()
	{
		while (this.ID < this.Rows * this.Columns)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Tile, new Vector3((float)this.Row, 0f, (float)this.Column), Quaternion.identity);
			gameObject.transform.parent = base.transform;
			this.Row++;
			if (this.Row > this.Rows)
			{
				this.Row = 1;
				this.Column++;
			}
			this.ID++;
		}
		base.transform.localScale = new Vector3(4f, 4f, 4f);
		base.transform.position = new Vector3(-52f, 0f, -52f);
	}

	// Token: 0x0400208A RID: 8330
	public GameObject Tile;

	// Token: 0x0400208B RID: 8331
	public int Row;

	// Token: 0x0400208C RID: 8332
	public int Column;

	// Token: 0x0400208D RID: 8333
	public int Rows = 25;

	// Token: 0x0400208E RID: 8334
	public int Columns = 25;

	// Token: 0x0400208F RID: 8335
	public int ID;
}
