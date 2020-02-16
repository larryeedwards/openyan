using System;
using UnityEngine;

// Token: 0x020003B0 RID: 944
public class EmptyHuskScript : MonoBehaviour
{
	// Token: 0x0600192F RID: 6447 RVA: 0x000E85E4 File Offset: 0x000E69E4
	private void Update()
	{
		if (this.EatPhase < this.BloodTimes.Length && this.MyAnimation["f02_sixEat_00"].time > this.BloodTimes[this.EatPhase])
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TargetStudent.StabBloodEffect, this.Mouth.position, Quaternion.identity);
			gameObject.GetComponent<RandomStabScript>().Biting = true;
			this.EatPhase++;
		}
		if (this.MyAnimation["f02_sixEat_00"].time >= this.MyAnimation["f02_sixEat_00"].length)
		{
			if (this.DarkAura != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DarkAura, base.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001D43 RID: 7491
	public StudentScript TargetStudent;

	// Token: 0x04001D44 RID: 7492
	public Animation MyAnimation;

	// Token: 0x04001D45 RID: 7493
	public GameObject DarkAura;

	// Token: 0x04001D46 RID: 7494
	public Transform Mouth;

	// Token: 0x04001D47 RID: 7495
	public float[] BloodTimes;

	// Token: 0x04001D48 RID: 7496
	public int EatPhase;
}
