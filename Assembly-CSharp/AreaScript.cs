using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class AreaScript : MonoBehaviour
{
	// Token: 0x06001711 RID: 5905 RVA: 0x000B2754 File Offset: 0x000B0B54
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Student"))
		{
			StudentScript component = other.GetComponent<StudentScript>();
			this.Students.Add(component);
			this.Population++;
		}
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x000B2794 File Offset: 0x000B0B94
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Student"))
		{
			StudentScript component = other.GetComponent<StudentScript>();
			this.Students.Remove(component);
			this.Population--;
		}
	}

	// Token: 0x04001649 RID: 5705
	[Header("Do not touch any of these values. They get updated at runtime.")]
	[Tooltip("The amount of students in this area.")]
	public int Population;

	// Token: 0x0400164A RID: 5706
	[Tooltip("A list of students in this area.")]
	public List<StudentScript> Students;

	// Token: 0x0400164B RID: 5707
	[Tooltip("This area's crowd. Students will go here.")]
	public List<StudentScript> Crowd;
}
