using System;
using UnityEngine;

// Token: 0x0200047A RID: 1146
public class ObstacleDetectorScript : MonoBehaviour
{
	// Token: 0x06001E09 RID: 7689 RVA: 0x00122160 File Offset: 0x00120560
	private void Start()
	{
		this.ControllerX.SetActive(false);
		this.KeyboardX.SetActive(false);
	}

	// Token: 0x0400263A RID: 9786
	public YandereScript Yandere;

	// Token: 0x0400263B RID: 9787
	public GameObject ControllerX;

	// Token: 0x0400263C RID: 9788
	public GameObject KeyboardX;

	// Token: 0x0400263D RID: 9789
	public Collider[] ObstacleArray;

	// Token: 0x0400263E RID: 9790
	public int Obstacles;

	// Token: 0x0400263F RID: 9791
	public bool Add;

	// Token: 0x04002640 RID: 9792
	public int ID;
}
