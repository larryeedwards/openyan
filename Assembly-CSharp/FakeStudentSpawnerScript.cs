using System;
using UnityEngine;

// Token: 0x020003CD RID: 973
public class FakeStudentSpawnerScript : MonoBehaviour
{
	// Token: 0x0600198F RID: 6543 RVA: 0x000EE658 File Offset: 0x000ECA58
	public void Spawn()
	{
		if (!this.AlreadySpawned)
		{
			this.Student = this.FakeFemale;
			this.NESW = 1;
			while (this.Spawned < 100)
			{
				if (this.NESW == 1)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-21f, 21f), (float)this.Height, UnityEngine.Random.Range(21f, 19f)), Quaternion.identity);
				}
				else if (this.NESW == 2)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(19f, 21f), (float)this.Height, UnityEngine.Random.Range(29f, -37f)), Quaternion.identity);
				}
				else if (this.NESW == 3)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-21f, 21f), (float)this.Height, UnityEngine.Random.Range(-21f, -19f)), Quaternion.identity);
				}
				else if (this.NESW == 4)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-19f, -21f), (float)this.Height, UnityEngine.Random.Range(29f, -37f)), Quaternion.identity);
				}
				this.StudentID++;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().FakeStudentSpawner = this;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().StudentID = this.StudentID;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().NESW = this.NESW;
				this.NewStudent.transform.parent = this.FakeStudentParent;
				this.CurrentFloor++;
				this.CurrentRow++;
				this.Spawned++;
				if (this.CurrentFloor == this.FloorLimit)
				{
					this.CurrentFloor = 0;
					this.Height += 4;
				}
				if (this.CurrentRow == this.RowLimit)
				{
					this.CurrentRow = 0;
					this.NESW++;
					if (this.NESW > 4)
					{
						this.NESW = 1;
					}
				}
				this.Student = ((!(this.Student == this.FakeFemale)) ? this.FakeFemale : this.FakeMale);
			}
			this.StudentIDLimit = this.StudentID;
			this.StudentID = 1;
			this.AlreadySpawned = true;
		}
		else
		{
			this.FakeStudentParent.gameObject.SetActive(!this.FakeStudentParent.gameObject.activeInHierarchy);
		}
	}

	// Token: 0x04001E63 RID: 7779
	public Transform FakeStudentParent;

	// Token: 0x04001E64 RID: 7780
	public GameObject NewStudent;

	// Token: 0x04001E65 RID: 7781
	public GameObject FakeFemale;

	// Token: 0x04001E66 RID: 7782
	public GameObject FakeMale;

	// Token: 0x04001E67 RID: 7783
	public GameObject Student;

	// Token: 0x04001E68 RID: 7784
	public bool AlreadySpawned;

	// Token: 0x04001E69 RID: 7785
	public int CurrentFloor;

	// Token: 0x04001E6A RID: 7786
	public int CurrentRow;

	// Token: 0x04001E6B RID: 7787
	public int FloorLimit;

	// Token: 0x04001E6C RID: 7788
	public int RowLimit;

	// Token: 0x04001E6D RID: 7789
	public int StudentIDLimit;

	// Token: 0x04001E6E RID: 7790
	public int StudentID;

	// Token: 0x04001E6F RID: 7791
	public int Spawned;

	// Token: 0x04001E70 RID: 7792
	public int Height;

	// Token: 0x04001E71 RID: 7793
	public int NESW;

	// Token: 0x04001E72 RID: 7794
	public int ID;

	// Token: 0x04001E73 RID: 7795
	public GameObject[] SuspiciousObjects;
}
