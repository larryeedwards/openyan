using System;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class CameraShake : MonoBehaviour
{
	// Token: 0x060017AF RID: 6063 RVA: 0x000BCB89 File Offset: 0x000BAF89
	private void Awake()
	{
		if (this.camTransform == null)
		{
			this.camTransform = base.GetComponent<Transform>();
		}
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x000BCBA8 File Offset: 0x000BAFA8
	private void OnEnable()
	{
		this.originalPos = this.camTransform.localPosition;
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x000BCBBC File Offset: 0x000BAFBC
	private void Update()
	{
		if (this.shake > 0f)
		{
			this.camTransform.localPosition = this.originalPos + UnityEngine.Random.insideUnitSphere * this.shakeAmount;
			this.shake -= 0.0166666675f * this.decreaseFactor;
		}
		else
		{
			this.shake = 0f;
			this.camTransform.localPosition = this.originalPos;
		}
	}

	// Token: 0x040017AD RID: 6061
	public Transform camTransform;

	// Token: 0x040017AE RID: 6062
	public float shake;

	// Token: 0x040017AF RID: 6063
	public float shakeAmount = 0.7f;

	// Token: 0x040017B0 RID: 6064
	public float decreaseFactor = 1f;

	// Token: 0x040017B1 RID: 6065
	private Vector3 originalPos;
}
