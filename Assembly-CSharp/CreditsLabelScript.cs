using System;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class CreditsLabelScript : MonoBehaviour
{
	// Token: 0x06001851 RID: 6225 RVA: 0x000D4DC8 File Offset: 0x000D31C8
	private void Start()
	{
		this.Rotation = -90f;
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x000D4E1C File Offset: 0x000D321C
	private void Update()
	{
		this.Rotation += Time.deltaTime * this.RotationSpeed;
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y + Time.deltaTime * this.MovementSpeed, base.transform.localPosition.z);
		if (this.Rotation > 90f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001AB8 RID: 6840
	public float RotationSpeed;

	// Token: 0x04001AB9 RID: 6841
	public float MovementSpeed;

	// Token: 0x04001ABA RID: 6842
	public float Rotation;
}
