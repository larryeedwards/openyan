using System;
using UnityEngine;

// Token: 0x0200051F RID: 1311
public class StandPunchScript : MonoBehaviour
{
	// Token: 0x06002048 RID: 8264 RVA: 0x00150324 File Offset: 0x0014E724
	private void OnTriggerEnter(Collider other)
	{
		StudentScript component = other.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID > 1)
		{
			component.JojoReact();
		}
	}

	// Token: 0x04002D2C RID: 11564
	public Collider MyCollider;
}
