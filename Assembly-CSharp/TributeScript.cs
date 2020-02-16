using System;
using UnityEngine;

// Token: 0x0200055E RID: 1374
public class TributeScript : MonoBehaviour
{
	// Token: 0x060021CD RID: 8653 RVA: 0x0019979E File Offset: 0x00197B9E
	private void Start()
	{
		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode)
		{
			base.enabled = false;
		}
		this.Rainey.SetActive(false);
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x001997C8 File Offset: 0x00197BC8
	private void Update()
	{
		if (this.RiggedAttacher.gameObject.activeInHierarchy)
		{
			this.RiggedAttacher.newRenderer.SetBlendShapeWeight(0, 100f);
			this.RiggedAttacher.newRenderer.SetBlendShapeWeight(1, 100f);
			base.enabled = false;
		}
		else if (!this.Yandere.PauseScreen.Show && this.Yandere.CanMove)
		{
			if (Input.GetKeyDown(this.Letter[this.ID]))
			{
				this.ID++;
				if (this.ID == this.Letter.Length)
				{
					this.Rainey.SetActive(true);
					base.enabled = false;
				}
			}
			if (Input.GetKeyDown(this.AzurLane[this.AzurID]))
			{
				this.AzurID++;
				if (this.AzurID == this.AzurLane.Length)
				{
					this.Yandere.AzurLane();
					base.enabled = false;
				}
			}
			if (Input.GetKeyDown(this.NurseLetters[this.NurseID]))
			{
				this.NurseID++;
				if (this.NurseID == this.NurseLetters.Length)
				{
					this.RiggedAttacher.root = this.StudentManager.Students[90].Hips.parent.gameObject;
					this.RiggedAttacher.Student = this.StudentManager.Students[90];
					this.RiggedAttacher.gameObject.SetActive(true);
					this.StudentManager.Students[90].MyRenderer.enabled = false;
				}
			}
			if (this.Yandere.Armed && this.Yandere.EquippedWeapon.WeaponID == 14 && Input.GetKeyDown(this.MiyukiLetters[this.MiyukiID]))
			{
				this.MiyukiID++;
				if (this.MiyukiID == this.MiyukiLetters.Length)
				{
					this.Henshin.TransformYandere();
					this.Yandere.CanMove = false;
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x040036DC RID: 14044
	public RiggedAccessoryAttacher RiggedAttacher;

	// Token: 0x040036DD RID: 14045
	public StudentManagerScript StudentManager;

	// Token: 0x040036DE RID: 14046
	public HenshinScript Henshin;

	// Token: 0x040036DF RID: 14047
	public YandereScript Yandere;

	// Token: 0x040036E0 RID: 14048
	public GameObject Rainey;

	// Token: 0x040036E1 RID: 14049
	public string[] MiyukiLetters;

	// Token: 0x040036E2 RID: 14050
	public string[] NurseLetters;

	// Token: 0x040036E3 RID: 14051
	public string[] AzurLane;

	// Token: 0x040036E4 RID: 14052
	public string[] Letter;

	// Token: 0x040036E5 RID: 14053
	public int MiyukiID;

	// Token: 0x040036E6 RID: 14054
	public int NurseID;

	// Token: 0x040036E7 RID: 14055
	public int AzurID;

	// Token: 0x040036E8 RID: 14056
	public int ID;

	// Token: 0x040036E9 RID: 14057
	public Mesh ThiccMesh;
}
