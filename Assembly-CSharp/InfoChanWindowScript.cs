using System;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class InfoChanWindowScript : MonoBehaviour
{
	// Token: 0x06001CF5 RID: 7413 RVA: 0x0010B8EC File Offset: 0x00109CEC
	private void Update()
	{
		if (this.Drop)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, (!this.Drop) ? 0f : -90f, Time.deltaTime * 10f);
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				if ((float)this.Orders > 0f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.Drops[this.ItemsToDrop[this.Orders]], this.DropPoint.position, Quaternion.identity);
					this.Timer = 0f;
					this.Orders--;
				}
				else
				{
					this.Open = false;
					if (this.Timer > 3f)
					{
						base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 0f, base.transform.localEulerAngles.z);
						this.Drop = false;
					}
				}
			}
		}
		if (this.Test)
		{
			this.DropObject();
		}
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x0010BA5B File Offset: 0x00109E5B
	public void DropObject()
	{
		this.Rotation = 0f;
		this.Timer = 0f;
		this.Dropped = false;
		this.Test = false;
		this.Drop = true;
		this.Open = true;
	}

	// Token: 0x040022E3 RID: 8931
	public Transform DropPoint;

	// Token: 0x040022E4 RID: 8932
	public GameObject[] Drops;

	// Token: 0x040022E5 RID: 8933
	public int[] ItemsToDrop;

	// Token: 0x040022E6 RID: 8934
	public int Orders;

	// Token: 0x040022E7 RID: 8935
	public int ID;

	// Token: 0x040022E8 RID: 8936
	public float Rotation;

	// Token: 0x040022E9 RID: 8937
	public float Timer;

	// Token: 0x040022EA RID: 8938
	public bool Dropped;

	// Token: 0x040022EB RID: 8939
	public bool Drop;

	// Token: 0x040022EC RID: 8940
	public bool Open = true;

	// Token: 0x040022ED RID: 8941
	public bool Test;
}
