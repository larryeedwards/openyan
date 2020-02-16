using System;
using UnityEngine;

// Token: 0x0200051C RID: 1308
public class StalkerPromptScript : MonoBehaviour
{
	// Token: 0x0600203F RID: 8255 RVA: 0x0014F8E0 File Offset: 0x0014DCE0
	private void Update()
	{
		base.transform.LookAt(this.Yandere.MainCamera.transform);
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
			if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 2f && Input.GetButtonDown("A") && this.ID == 1)
			{
				this.Yandere.MyAnimation.CrossFade("f02_climbTrellis_00");
				this.Yandere.Climbing = true;
				this.Yandere.CanMove = false;
				UnityEngine.Object.Destroy(base.gameObject);
				UnityEngine.Object.Destroy(this.MySprite);
			}
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
		}
		this.MySprite.color = new Color(1f, 1f, 1f, this.Alpha);
	}

	// Token: 0x04002D0B RID: 11531
	public StalkerYandereScript Yandere;

	// Token: 0x04002D0C RID: 11532
	public UISprite MySprite;

	// Token: 0x04002D0D RID: 11533
	public float Alpha;

	// Token: 0x04002D0E RID: 11534
	public int ID;
}
