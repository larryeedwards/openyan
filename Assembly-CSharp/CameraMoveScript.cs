using System;
using UnityEngine;

// Token: 0x020005CB RID: 1483
public class CameraMoveScript : MonoBehaviour
{
	// Token: 0x0600237E RID: 9086 RVA: 0x001C1656 File Offset: 0x001BFA56
	private void Start()
	{
		base.transform.position = this.StartPos.position;
		base.transform.rotation = this.StartPos.rotation;
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x001C1684 File Offset: 0x001BFA84
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			this.Timer += Time.deltaTime * this.Speed;
			if (this.Timer > 0.1f)
			{
				this.OpenDoors = true;
				if (this.LeftDoor != null)
				{
					this.LeftDoor.transform.localPosition = new Vector3(Mathf.Lerp(this.LeftDoor.transform.localPosition.x, 1f, Time.deltaTime), this.LeftDoor.transform.localPosition.y, this.LeftDoor.transform.localPosition.z);
					this.RightDoor.transform.localPosition = new Vector3(Mathf.Lerp(this.RightDoor.transform.localPosition.x, -1f, Time.deltaTime), this.RightDoor.transform.localPosition.y, this.RightDoor.transform.localPosition.z);
				}
			}
			base.transform.position = Vector3.Lerp(base.transform.position, this.EndPos.position, Time.deltaTime * this.Timer);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.EndPos.rotation, Time.deltaTime * this.Timer);
		}
	}

	// Token: 0x06002380 RID: 9088 RVA: 0x001C182E File Offset: 0x001BFC2E
	private void LateUpdate()
	{
		if (this.Target != null)
		{
			base.transform.LookAt(this.Target);
		}
	}

	// Token: 0x04003D78 RID: 15736
	public Transform StartPos;

	// Token: 0x04003D79 RID: 15737
	public Transform EndPos;

	// Token: 0x04003D7A RID: 15738
	public Transform RightDoor;

	// Token: 0x04003D7B RID: 15739
	public Transform LeftDoor;

	// Token: 0x04003D7C RID: 15740
	public Transform Target;

	// Token: 0x04003D7D RID: 15741
	public bool OpenDoors;

	// Token: 0x04003D7E RID: 15742
	public bool Begin;

	// Token: 0x04003D7F RID: 15743
	public float Speed;

	// Token: 0x04003D80 RID: 15744
	public float Timer;
}
