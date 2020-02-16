using System;
using UnityEngine;

// Token: 0x02000562 RID: 1378
public class UpthrustScript : MonoBehaviour
{
	// Token: 0x060021DB RID: 8667 RVA: 0x0019AAED File Offset: 0x00198EED
	private void Start()
	{
		this.startPosition = base.transform.localPosition;
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x0019AB00 File Offset: 0x00198F00
	private void Update()
	{
		float d = this.amplitude * Mathf.Sin(6.28318548f * this.frequency * Time.time);
		base.transform.localPosition = this.startPosition + this.evaluatePosition(Time.time);
		base.transform.Rotate(this.rotationAmplitude * d);
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x0019AB64 File Offset: 0x00198F64
	private Vector3 evaluatePosition(float time)
	{
		float y = this.amplitude * Mathf.Sin(6.28318548f * this.frequency * time);
		return new Vector3(0f, y, 0f);
	}

	// Token: 0x04003750 RID: 14160
	[SerializeField]
	private float amplitude = 0.1f;

	// Token: 0x04003751 RID: 14161
	[SerializeField]
	private float frequency = 0.6f;

	// Token: 0x04003752 RID: 14162
	[SerializeField]
	private Vector3 rotationAmplitude = new Vector3(4.45f, 4.45f, 4.45f);

	// Token: 0x04003753 RID: 14163
	private Vector3 startPosition;
}
