using System;
using UnityEngine;

// Token: 0x02000542 RID: 1346
public class TarpScript : MonoBehaviour
{
	// Token: 0x06002165 RID: 8549 RVA: 0x00193A31 File Offset: 0x00191E31
	private void Start()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x00193A54 File Offset: 0x00191E54
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			AudioSource.PlayClipAtPoint(this.Tarp, base.transform.position);
			this.Unwrap = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Mecha.enabled = true;
			this.Mecha.Prompt.enabled = true;
		}
		if (this.Unwrap)
		{
			this.Speed += Time.deltaTime * 10f;
			base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, new Vector3(90f, 90f, 0f), Time.deltaTime * this.Speed);
			if (base.transform.localEulerAngles.x > 45f)
			{
				if (this.PreviousSpeed == 0f)
				{
					this.PreviousSpeed = this.Speed;
				}
				this.Speed += Time.deltaTime * 10f;
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 0.0001f), (this.Speed - this.PreviousSpeed) * Time.deltaTime);
			}
		}
	}

	// Token: 0x040035D4 RID: 13780
	public PromptScript Prompt;

	// Token: 0x040035D5 RID: 13781
	public MechaScript Mecha;

	// Token: 0x040035D6 RID: 13782
	public AudioClip Tarp;

	// Token: 0x040035D7 RID: 13783
	public float PreviousSpeed;

	// Token: 0x040035D8 RID: 13784
	public float Speed;

	// Token: 0x040035D9 RID: 13785
	public bool Unwrap;
}
