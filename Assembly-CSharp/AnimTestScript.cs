using System;
using UnityEngine;

// Token: 0x02000320 RID: 800
public class AnimTestScript : MonoBehaviour
{
	// Token: 0x060016F6 RID: 5878 RVA: 0x000B1734 File Offset: 0x000AFB34
	private void Start()
	{
		Time.timeScale = 1f;
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x000B1740 File Offset: 0x000AFB40
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.ID++;
			if (this.ID > 4)
			{
				this.ID = 1;
			}
		}
		if (this.ID == 1)
		{
			this.CharacterB.transform.eulerAngles = new Vector3(0f, -90f, 0f);
			this.CharacterA.Play("f02_weightHighSanityA_00");
			this.CharacterB.Play("f02_weightHighSanityB_00");
		}
		else if (this.ID == 2)
		{
			this.CharacterA.Play("f02_weightMedSanityA_00");
			this.CharacterB.Play("f02_weightMedSanityB_00");
		}
		else if (this.ID == 3)
		{
			this.CharacterA.Play("f02_weightLowSanityA_00");
			this.CharacterB.Play("f02_weightLowSanityB_00");
		}
		else if (this.ID == 4)
		{
			this.CharacterB.transform.eulerAngles = new Vector3(0f, 90f, 0f);
			this.CharacterA.Play("f02_weightStealthA_00");
			this.CharacterB.Play("f02_weightStealthB_00");
		}
	}

	// Token: 0x04001608 RID: 5640
	public Animation CharacterA;

	// Token: 0x04001609 RID: 5641
	public Animation CharacterB;

	// Token: 0x0400160A RID: 5642
	public int ID;
}
