using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200049E RID: 1182
public class PopulationManagerScript : MonoBehaviour
{
	// Token: 0x06001E99 RID: 7833 RVA: 0x0012F274 File Offset: 0x0012D674
	public Vector3 GetCrowdedLocation()
	{
		AreaScript crowdedArea = this.GetCrowdedArea();
		Vector3 position = crowdedArea.transform.position;
		Vector3 a = new Vector3(0f, 0f, 0f);
		float num = 0f;
		foreach (StudentScript studentScript in crowdedArea.Students)
		{
			a += new Vector3(studentScript.transform.position.x, 0f, studentScript.transform.position.z);
			num += 1f;
		}
		a /= num;
		int num2;
		if (position.y >= 0f && position.y < 4f)
		{
			num2 = 0;
		}
		else if (position.y >= 4f && position.y < 8f)
		{
			num2 = 4;
		}
		else if (position.y >= 8f && position.y < 12f)
		{
			num2 = 8;
		}
		else
		{
			num2 = 12;
		}
		return new Vector3(a.x, (float)num2, a.z);
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x0012F3E0 File Offset: 0x0012D7E0
	public AreaScript GetCrowdedArea()
	{
		AreaScript result = null;
		float num = 0f;
		foreach (AreaScript areaScript in this._definedAreas)
		{
			int population = areaScript.Population;
			if ((float)population > num)
			{
				num = (float)population;
				result = areaScript;
			}
		}
		return result;
	}

	// Token: 0x04002808 RID: 10248
	[Tooltip("All defined areas should go in here. If your area is not in here, it will not count as an actual area.")]
	[SerializeField]
	private List<AreaScript> _definedAreas;

	// Token: 0x04002809 RID: 10249
	public Transform Cube;
}
