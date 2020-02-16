using System;
using UnityEngine;

// Token: 0x0200054C RID: 1356
public class TimePortalScript : MonoBehaviour
{
	// Token: 0x06002189 RID: 8585 RVA: 0x00195914 File Offset: 0x00193D14
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Suck = true;
		}
		if (this.Suck)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackHole, base.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1.1f)
			{
				this.Delinquent[this.ID].Suck = true;
				this.Timer = 1f;
				this.ID++;
				if (this.ID > 9)
				{
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x0400362B RID: 13867
	public DelinquentScript[] Delinquent;

	// Token: 0x0400362C RID: 13868
	public GameObject BlackHole;

	// Token: 0x0400362D RID: 13869
	public float Timer;

	// Token: 0x0400362E RID: 13870
	public bool Suck;

	// Token: 0x0400362F RID: 13871
	public int ID;
}
