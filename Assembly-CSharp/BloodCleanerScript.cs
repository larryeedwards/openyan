using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000338 RID: 824
public class BloodCleanerScript : MonoBehaviour
{
	// Token: 0x0600174D RID: 5965 RVA: 0x000B7B1C File Offset: 0x000B5F1C
	private void Start()
	{
		Physics.IgnoreLayerCollision(11, 15, true);
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000B7B28 File Offset: 0x000B5F28
	private void Update()
	{
		if (this.Blood < 100f && this.BloodParent.childCount > 0)
		{
			this.Pathfinding.target = this.BloodParent.GetChild(0);
			this.Pathfinding.speed = 4f;
			if (this.Pathfinding.target.position.y < 4f)
			{
				this.Label.text = "1";
			}
			else if (this.Pathfinding.target.position.y < 8f)
			{
				this.Label.text = "2";
			}
			else if (this.Pathfinding.target.position.y < 12f)
			{
				this.Label.text = "3";
			}
			else
			{
				this.Label.text = "R";
			}
			if (this.Pathfinding.target != null)
			{
				this.Distance = Vector3.Distance(base.transform.position, this.Pathfinding.target.position);
				if (this.Distance < 0.45f)
				{
					this.Pathfinding.speed = 0f;
					Transform child = this.BloodParent.GetChild(0);
					if (child.GetComponent("BloodPoolScript") != null)
					{
						child.localScale = new Vector3(child.localScale.x - Time.deltaTime, child.localScale.y - Time.deltaTime, child.localScale.z);
						this.Blood += Time.deltaTime;
						if (this.Blood >= 100f)
						{
							this.Lens.SetActive(true);
						}
						if (child.transform.localScale.x < 0.1f)
						{
							UnityEngine.Object.Destroy(child.gameObject);
						}
					}
					else
					{
						UnityEngine.Object.Destroy(child.gameObject);
					}
				}
				else
				{
					this.Pathfinding.speed = 4f;
				}
			}
		}
	}

	// Token: 0x040016C6 RID: 5830
	public Transform BloodParent;

	// Token: 0x040016C7 RID: 5831
	public PromptScript Prompt;

	// Token: 0x040016C8 RID: 5832
	public AIPath Pathfinding;

	// Token: 0x040016C9 RID: 5833
	public GameObject Lens;

	// Token: 0x040016CA RID: 5834
	public UILabel Label;

	// Token: 0x040016CB RID: 5835
	public float Distance;

	// Token: 0x040016CC RID: 5836
	public float Blood;
}
