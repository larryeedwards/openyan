using System;
using UnityEngine;

// Token: 0x020003D4 RID: 980
public class FloatScript : MonoBehaviour
{
	// Token: 0x060019A2 RID: 6562 RVA: 0x000EFAE4 File Offset: 0x000EDEE4
	private void Update()
	{
		if (!this.Down)
		{
			this.Float += Time.deltaTime * this.Speed;
			if (this.Float > this.Limit)
			{
				this.Down = true;
			}
		}
		else
		{
			this.Float -= Time.deltaTime * this.Speed;
			if (this.Float < -1f * this.Limit)
			{
				this.Down = false;
			}
		}
		base.transform.localPosition += new Vector3(0f, this.Float * Time.deltaTime, 0f);
		if (base.transform.localPosition.y > this.UpLimit)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.UpLimit, base.transform.localPosition.z);
		}
		if (base.transform.localPosition.y < this.DownLimit)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.DownLimit, base.transform.localPosition.z);
		}
	}

	// Token: 0x04001EA4 RID: 7844
	public bool Down;

	// Token: 0x04001EA5 RID: 7845
	public float Float;

	// Token: 0x04001EA6 RID: 7846
	public float Speed;

	// Token: 0x04001EA7 RID: 7847
	public float Limit;

	// Token: 0x04001EA8 RID: 7848
	public float DownLimit;

	// Token: 0x04001EA9 RID: 7849
	public float UpLimit;
}
