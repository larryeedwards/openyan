using System;
using UnityEngine;

// Token: 0x02000480 RID: 1152
public class OtherScript : MonoBehaviour
{
	// Token: 0x06001E1B RID: 7707 RVA: 0x00122FE0 File Offset: 0x001213E0
	private void Start()
	{
		for (int i = 1; i < 101; i++)
		{
			if (!this.StudentManager.Students[i].Male && (this.StudentManager.Students[i].Cosmetic.Hairstyle == 20 || this.StudentManager.Students[i].Cosmetic.Hairstyle == 21))
			{
				this.Wow();
			}
		}
		if (!this.Other.gameObject.activeInHierarchy)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x00123078 File Offset: 0x00121478
	private void Update()
	{
		if (this.Other.gameObject.activeInHierarchy)
		{
			this.Speed += Time.deltaTime * 0.01f;
			this.Other.position = Vector3.MoveTowards(this.Other.position, this.Yandere.position, Time.deltaTime * this.Speed);
			this.Other.LookAt(this.Yandere.position);
			if (Vector3.Distance(this.Other.position, this.Yandere.position) < 0.5f)
			{
				Application.Quit();
			}
		}
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x00123124 File Offset: 0x00121524
	private void Wow()
	{
		if (!this.Other.gameObject.activeInHierarchy)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0f;
			this.StudentManager.SetAtmosphere();
			foreach (StudentScript studentScript in this.StudentManager.Students)
			{
				if (studentScript != null)
				{
					studentScript.gameObject.SetActive(false);
				}
			}
			this.Yandere.gameObject.GetComponent<YandereScript>().NoDebug = true;
			this.Other.gameObject.SetActive(true);
			this.Jukebox.SetActive(false);
			this.HUD.enabled = false;
		}
	}

	// Token: 0x0400266B RID: 9835
	public StudentManagerScript StudentManager;

	// Token: 0x0400266C RID: 9836
	public JsonScript JSON;

	// Token: 0x0400266D RID: 9837
	public UIPanel HUD;

	// Token: 0x0400266E RID: 9838
	public GameObject Jukebox;

	// Token: 0x0400266F RID: 9839
	public Transform Yandere;

	// Token: 0x04002670 RID: 9840
	public Transform Other;

	// Token: 0x04002671 RID: 9841
	public float Speed;
}
