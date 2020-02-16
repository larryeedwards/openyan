using System;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class BlasterScript : MonoBehaviour
{
	// Token: 0x06001747 RID: 5959 RVA: 0x000B7944 File Offset: 0x000B5D44
	private void Start()
	{
		this.Skull.localScale = Vector3.zero;
		this.Beam.localScale = Vector3.zero;
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x000B7968 File Offset: 0x000B5D68
	private void Update()
	{
		AnimationState animationState = base.GetComponent<Animation>()["Blast"];
		if (animationState.time > 1f)
		{
			this.Beam.localScale = Vector3.Lerp(this.Beam.localScale, new Vector3(15f, 1f, 1f), Time.deltaTime * 10f);
			this.Eyes.material.color = new Color(1f, 0f, 0f, 1f);
		}
		if (animationState.time >= animationState.length)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x000B7A18 File Offset: 0x000B5E18
	private void LateUpdate()
	{
		AnimationState animationState = base.GetComponent<Animation>()["Blast"];
		this.Size = ((animationState.time >= 1.5f) ? Mathf.Lerp(this.Size, 0f, Time.deltaTime * 10f) : Mathf.Lerp(this.Size, 2f, Time.deltaTime * 5f));
		this.Skull.localScale = new Vector3(this.Size, this.Size, this.Size);
	}

	// Token: 0x040016BF RID: 5823
	public Transform Skull;

	// Token: 0x040016C0 RID: 5824
	public Renderer Eyes;

	// Token: 0x040016C1 RID: 5825
	public Transform Beam;

	// Token: 0x040016C2 RID: 5826
	public float Size;
}
