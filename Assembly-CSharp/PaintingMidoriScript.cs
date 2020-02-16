using System;
using UnityEngine;

// Token: 0x02000483 RID: 1155
public class PaintingMidoriScript : MonoBehaviour
{
	// Token: 0x06001E24 RID: 7716 RVA: 0x001232E0 File Offset: 0x001216E0
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.ID++;
		}
		if (this.ID == 0)
		{
			this.Anim.CrossFade("f02_painting_00");
		}
		else if (this.ID == 1)
		{
			this.Anim.CrossFade("f02_shock_00");
			this.Rotation = Mathf.Lerp(this.Rotation, -180f, Time.deltaTime * 10f);
		}
		else if (this.ID == 2)
		{
			base.transform.position -= new Vector3(Time.deltaTime * 2f, 0f, 0f);
		}
		base.transform.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
	}

	// Token: 0x04002676 RID: 9846
	public Animation Anim;

	// Token: 0x04002677 RID: 9847
	public float Rotation;

	// Token: 0x04002678 RID: 9848
	public int ID;
}
