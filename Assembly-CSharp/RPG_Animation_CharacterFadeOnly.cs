using System;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class RPG_Animation_CharacterFadeOnly : MonoBehaviour
{
	// Token: 0x060016C7 RID: 5831 RVA: 0x000AE6EE File Offset: 0x000ACAEE
	private void Awake()
	{
		RPG_Animation_CharacterFadeOnly.instance = this;
	}

	// Token: 0x0400146C RID: 5228
	public static RPG_Animation_CharacterFadeOnly instance;
}
