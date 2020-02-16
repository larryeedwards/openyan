using System;
using UnityEngine;

// Token: 0x020003D8 RID: 984
public class FootprintScript : MonoBehaviour
{
	// Token: 0x060019AB RID: 6571 RVA: 0x000F00D4 File Offset: 0x000EE4D4
	private void Start()
	{
		if (this.Yandere.Schoolwear == 0 || this.Yandere.Schoolwear == 2 || (this.Yandere.ClubAttire && ClubGlobals.Club == ClubType.MartialArts) || this.Yandere.Hungry || this.Yandere.LucyHelmet.activeInHierarchy)
		{
			base.GetComponent<Renderer>().material.mainTexture = this.Footprint;
		}
		if (GameGlobals.CensorBlood)
		{
			base.GetComponent<Renderer>().material.mainTexture = this.Flower;
			base.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x04001EBD RID: 7869
	public YandereScript Yandere;

	// Token: 0x04001EBE RID: 7870
	public Texture Footprint;

	// Token: 0x04001EBF RID: 7871
	public Texture Flower;
}
