using System;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class BuildingDestructionScript : MonoBehaviour
{
	// Token: 0x06001797 RID: 6039 RVA: 0x000BAD38 File Offset: 0x000B9138
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Phase++;
			this.Sink = true;
		}
		if (this.Sink)
		{
			if (this.Phase == 1)
			{
				base.transform.position = new Vector3(UnityEngine.Random.Range(-1f, 1f), base.transform.position.y - Time.deltaTime * 10f, UnityEngine.Random.Range(-19f, -21f));
			}
			else if (this.NewSchool.position.y != 0f)
			{
				this.NewSchool.position = new Vector3(this.NewSchool.position.x, Mathf.MoveTowards(this.NewSchool.position.y, 0f, Time.deltaTime * 10f), this.NewSchool.position.z);
				base.transform.position = new Vector3(UnityEngine.Random.Range(-1f, 1f), base.transform.position.y, UnityEngine.Random.Range(13f, 15f));
			}
			else
			{
				base.transform.position = new Vector3(0f, base.transform.position.y, 14f);
			}
		}
	}

	// Token: 0x04001764 RID: 5988
	public Transform NewSchool;

	// Token: 0x04001765 RID: 5989
	public bool Sink;

	// Token: 0x04001766 RID: 5990
	public int Phase;
}
