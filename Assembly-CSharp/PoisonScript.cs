using System;
using UnityEngine;

// Token: 0x0200049A RID: 1178
public class PoisonScript : MonoBehaviour
{
	// Token: 0x06001E8A RID: 7818 RVA: 0x0012CFCE File Offset: 0x0012B3CE
	public void Start()
	{
		base.gameObject.SetActive(ClassGlobals.ChemistryGrade + ClassGlobals.ChemistryBonus >= 1);
	}

	// Token: 0x06001E8B RID: 7819 RVA: 0x0012CFEC File Offset: 0x0012B3EC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Inventory.ChemicalPoison = true;
			this.Yandere.StudentManager.UpdateAllBentos();
			UnityEngine.Object.Destroy(base.gameObject);
			UnityEngine.Object.Destroy(this.Bottle);
		}
	}

	// Token: 0x040027B5 RID: 10165
	public YandereScript Yandere;

	// Token: 0x040027B6 RID: 10166
	public PromptScript Prompt;

	// Token: 0x040027B7 RID: 10167
	public GameObject Bottle;
}
