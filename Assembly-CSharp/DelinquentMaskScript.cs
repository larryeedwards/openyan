using System;
using UnityEngine;

// Token: 0x0200038C RID: 908
public class DelinquentMaskScript : MonoBehaviour
{
	// Token: 0x060018B2 RID: 6322 RVA: 0x000DDC80 File Offset: 0x000DC080
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;
			if (this.ID > 4)
			{
				this.ID = 0;
			}
			this.MyRenderer.mesh = this.Meshes[this.ID];
		}
	}

	// Token: 0x04001BE5 RID: 7141
	public MeshFilter MyRenderer;

	// Token: 0x04001BE6 RID: 7142
	public Mesh[] Meshes;

	// Token: 0x04001BE7 RID: 7143
	public int ID;
}
