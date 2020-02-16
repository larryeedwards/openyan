using System;
using UnityEngine;

// Token: 0x02000456 RID: 1110
public class LowPolyStudentScript : MonoBehaviour
{
	// Token: 0x06001D87 RID: 7559 RVA: 0x001167C4 File Offset: 0x00114BC4
	private void Update()
	{
		if ((float)this.Student.StudentManager.LowDetailThreshold > 0f)
		{
			float distanceSqr = this.Student.Prompt.DistanceSqr;
			if (distanceSqr > (float)this.Student.StudentManager.LowDetailThreshold)
			{
				if (!this.MyMesh.enabled)
				{
					this.Student.MyRenderer.enabled = false;
					this.MyMesh.enabled = true;
				}
			}
			else if (this.MyMesh.enabled)
			{
				this.Student.MyRenderer.enabled = true;
				this.MyMesh.enabled = false;
			}
		}
		else if (this.MyMesh.enabled)
		{
			this.Student.MyRenderer.enabled = true;
			this.MyMesh.enabled = false;
		}
	}

	// Token: 0x040024E6 RID: 9446
	public StudentScript Student;

	// Token: 0x040024E7 RID: 9447
	public Renderer TeacherMesh;

	// Token: 0x040024E8 RID: 9448
	public Renderer MyMesh;
}
