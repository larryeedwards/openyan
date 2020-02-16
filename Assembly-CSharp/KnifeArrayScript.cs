using System;
using UnityEngine;

// Token: 0x02000448 RID: 1096
public class KnifeArrayScript : MonoBehaviour
{
	// Token: 0x06001D5C RID: 7516 RVA: 0x00112DD8 File Offset: 0x001111D8
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.ID < 10)
		{
			if (this.Timer > this.SpawnTimes[this.ID] && this.GlobalKnifeArray.ID < 1000)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Knife, base.transform.position, Quaternion.identity);
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0.5f, 2f), UnityEngine.Random.Range(-0.75f, -1.75f));
				gameObject.transform.parent = null;
				gameObject.transform.LookAt(this.KnifeTarget);
				this.GlobalKnifeArray.Knives[this.GlobalKnifeArray.ID] = gameObject.GetComponent<TimeStopKnifeScript>();
				this.GlobalKnifeArray.ID++;
				this.ID++;
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002451 RID: 9297
	public GlobalKnifeArrayScript GlobalKnifeArray;

	// Token: 0x04002452 RID: 9298
	public Transform KnifeTarget;

	// Token: 0x04002453 RID: 9299
	public float[] SpawnTimes;

	// Token: 0x04002454 RID: 9300
	public GameObject Knife;

	// Token: 0x04002455 RID: 9301
	public float Timer;

	// Token: 0x04002456 RID: 9302
	public int ID;
}
