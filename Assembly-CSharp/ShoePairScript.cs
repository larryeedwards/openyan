using System;
using UnityEngine;

// Token: 0x02000507 RID: 1287
public class ShoePairScript : MonoBehaviour
{
	// Token: 0x06001FF0 RID: 8176 RVA: 0x00148295 File Offset: 0x00146695
	private void Start()
	{
		this.Police = GameObject.Find("Police").GetComponent<PoliceScript>();
		if (ClassGlobals.LanguageGrade + ClassGlobals.LanguageBonus < 1)
		{
			this.Prompt.enabled = false;
		}
		this.Note.SetActive(false);
	}

	// Token: 0x06001FF1 RID: 8177 RVA: 0x001482D8 File Offset: 0x001466D8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Police.Suicide = true;
			this.Note.SetActive(true);
		}
	}

	// Token: 0x04002C10 RID: 11280
	public PoliceScript Police;

	// Token: 0x04002C11 RID: 11281
	public PromptScript Prompt;

	// Token: 0x04002C12 RID: 11282
	public GameObject Note;
}
