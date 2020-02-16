using System;
using UnityEngine;

// Token: 0x0200055C RID: 1372
public class TrashCompactorScript : MonoBehaviour
{
	// Token: 0x060021C6 RID: 8646 RVA: 0x0019947C File Offset: 0x0019787C
	private void Start()
	{
		if (this.StudentManager.Students[10] != null || this.StudentManager.Students[11] != null)
		{
			this.CompactTrash();
		}
		else
		{
			for (int i = 1; i < 101; i++)
			{
				if (this.StudentManager.Students[i] != null && !this.StudentManager.Students[i].Male && (this.StudentManager.Students[i].Cosmetic.Hairstyle == 20 || this.StudentManager.Students[i].Cosmetic.Hairstyle == 21 || this.StudentManager.Students[i].Persona == PersonaType.Protective))
				{
					this.CompactTrash();
				}
			}
		}
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x00199564 File Offset: 0x00197964
	private void Update()
	{
		if (this.TrashCompactorObject.gameObject.activeInHierarchy)
		{
			this.Speed += Time.deltaTime * 0.01f;
			this.TrashCompactorObject.position = Vector3.MoveTowards(this.TrashCompactorObject.position, this.Yandere.position, Time.deltaTime * this.Speed);
			this.TrashCompactorObject.LookAt(this.Yandere.position);
			if (Vector3.Distance(this.TrashCompactorObject.position, this.Yandere.position) < 0.5f)
			{
				Application.Quit();
			}
		}
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x00199610 File Offset: 0x00197A10
	private void CompactTrash()
	{
		Debug.Log("Taking out the garbage.");
		if (!this.TrashCompactorObject.gameObject.activeInHierarchy)
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
			this.TrashCompactorObject.gameObject.SetActive(true);
			this.Jukebox.SetActive(false);
			this.HUD.enabled = false;
		}
	}

	// Token: 0x040036D1 RID: 14033
	public StudentManagerScript StudentManager;

	// Token: 0x040036D2 RID: 14034
	public JsonScript JSON;

	// Token: 0x040036D3 RID: 14035
	public UIPanel HUD;

	// Token: 0x040036D4 RID: 14036
	public GameObject Jukebox;

	// Token: 0x040036D5 RID: 14037
	public Transform TrashCompactorObject;

	// Token: 0x040036D6 RID: 14038
	public Transform Yandere;

	// Token: 0x040036D7 RID: 14039
	public float Speed;
}
