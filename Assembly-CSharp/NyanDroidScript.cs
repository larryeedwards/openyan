using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000479 RID: 1145
public class NyanDroidScript : MonoBehaviour
{
	// Token: 0x06001E06 RID: 7686 RVA: 0x00121E90 File Offset: 0x00120290
	private void Start()
	{
		this.OriginalPosition = base.transform.position;
	}

	// Token: 0x06001E07 RID: 7687 RVA: 0x00121EA4 File Offset: 0x001202A4
	private void Update()
	{
		if (!this.Pathfinding.canSearch)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Stop";
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Timer = 0f;
				base.transform.position += new Vector3(0f, 0.0001f, 0f);
				if (base.transform.position.y < 0f)
				{
					base.transform.position = new Vector3(base.transform.position.x, 0.001f, base.transform.position.z);
				}
				Physics.SyncTransforms();
			}
			if (Input.GetButtonDown("RB"))
			{
				base.transform.position = this.OriginalPosition;
			}
			if (Vector3.Distance(base.transform.position, this.Pathfinding.target.position) <= 1f)
			{
				this.Character.CrossFade(this.Prefix + "_Idle");
				this.Pathfinding.speed = 0f;
			}
			else if (Vector3.Distance(base.transform.position, this.Pathfinding.target.position) <= 2f)
			{
				this.Character.CrossFade(this.Prefix + "_Walk");
				this.Pathfinding.speed = 0.5f;
			}
			else
			{
				this.Character.CrossFade(this.Prefix + "_Run");
				this.Pathfinding.speed = 5f;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Follow";
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Character.CrossFade(this.Prefix + "_Idle");
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
			}
		}
	}

	// Token: 0x04002634 RID: 9780
	public Animation Character;

	// Token: 0x04002635 RID: 9781
	public PromptScript Prompt;

	// Token: 0x04002636 RID: 9782
	public AIPath Pathfinding;

	// Token: 0x04002637 RID: 9783
	public Vector3 OriginalPosition;

	// Token: 0x04002638 RID: 9784
	public string Prefix;

	// Token: 0x04002639 RID: 9785
	public float Timer;
}
