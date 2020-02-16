using System;
using UnityEngine;

// Token: 0x020003DA RID: 986
public class FootstepScript : MonoBehaviour
{
	// Token: 0x060019B0 RID: 6576 RVA: 0x000F076E File Offset: 0x000EEB6E
	private void Start()
	{
		if (!this.Student.Nemesis)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x000F0788 File Offset: 0x000EEB88
	private void Update()
	{
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Student.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
			}
		}
		else if (base.transform.position.y < this.Student.transform.position.y + this.DownThreshold)
		{
			if (this.FootUp)
			{
				if (this.Student.Pathfinding.speed > 1f)
				{
					this.MyAudio.clip = this.RunFootsteps[UnityEngine.Random.Range(0, this.RunFootsteps.Length)];
					this.MyAudio.volume = 0.2f;
				}
				else
				{
					this.MyAudio.clip = this.WalkFootsteps[UnityEngine.Random.Range(0, this.WalkFootsteps.Length)];
					this.MyAudio.volume = 0.1f;
				}
				this.MyAudio.Play();
			}
			this.FootUp = false;
		}
	}

	// Token: 0x04001ED3 RID: 7891
	public StudentScript Student;

	// Token: 0x04001ED4 RID: 7892
	public AudioSource MyAudio;

	// Token: 0x04001ED5 RID: 7893
	public AudioClip[] WalkFootsteps;

	// Token: 0x04001ED6 RID: 7894
	public AudioClip[] RunFootsteps;

	// Token: 0x04001ED7 RID: 7895
	public float DownThreshold = 0.02f;

	// Token: 0x04001ED8 RID: 7896
	public float UpThreshold = 0.025f;

	// Token: 0x04001ED9 RID: 7897
	public bool FootUp;
}
