using System;
using UnityEngine;

// Token: 0x02000492 RID: 1170
public class PhotographyClubScript : MonoBehaviour
{
	// Token: 0x06001E6E RID: 7790 RVA: 0x0012AC6C File Offset: 0x0012906C
	private void Start()
	{
		if (SchoolGlobals.SchoolAtmosphere <= 0.8f)
		{
			this.InvestigationPhotos.SetActive(true);
			this.ArtsyPhotos.SetActive(false);
			this.CrimeScene.SetActive(true);
			this.StraightTables.SetActive(true);
			this.CrookedTables.SetActive(false);
		}
		else
		{
			this.InvestigationPhotos.SetActive(false);
			this.ArtsyPhotos.SetActive(true);
			this.CrimeScene.SetActive(false);
			this.StraightTables.SetActive(false);
			this.CrookedTables.SetActive(true);
		}
	}

	// Token: 0x0400274A RID: 10058
	public GameObject CrimeScene;

	// Token: 0x0400274B RID: 10059
	public GameObject InvestigationPhotos;

	// Token: 0x0400274C RID: 10060
	public GameObject ArtsyPhotos;

	// Token: 0x0400274D RID: 10061
	public GameObject StraightTables;

	// Token: 0x0400274E RID: 10062
	public GameObject CrookedTables;
}
