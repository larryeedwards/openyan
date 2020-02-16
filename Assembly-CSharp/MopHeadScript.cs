using System;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public class MopHeadScript : MonoBehaviour
{
	// Token: 0x06001DBE RID: 7614 RVA: 0x0011A844 File Offset: 0x00118C44
	private void OnTriggerStay(Collider other)
	{
		if (this.Mop.Bloodiness < 100f && other.tag == "Puddle")
		{
			this.BloodPool = other.gameObject.GetComponent<BloodPoolScript>();
			if (this.BloodPool != null)
			{
				this.BloodPool.Grow = false;
				other.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
				if (this.BloodPool.Blood)
				{
					this.Mop.Bloodiness += Time.deltaTime * 10f;
					this.Mop.UpdateBlood();
				}
				if (other.transform.localScale.x < 0.1f)
				{
					UnityEngine.Object.Destroy(other.gameObject);
				}
			}
			else
			{
				UnityEngine.Object.Destroy(other.gameObject);
			}
		}
	}

	// Token: 0x04002561 RID: 9569
	public BloodPoolScript BloodPool;

	// Token: 0x04002562 RID: 9570
	public MopScript Mop;
}
