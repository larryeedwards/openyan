using System;
using UnityEngine;

// Token: 0x02000363 RID: 867
public class ChemistScannerScript : MonoBehaviour
{
	// Token: 0x060017D1 RID: 6097 RVA: 0x000BDF90 File Offset: 0x000BC390
	private void Update()
	{
		if (this.Student.Ragdoll != null && this.Student.Ragdoll.enabled)
		{
			this.MyRenderer.materials[1].mainTexture = this.DeadEyes;
			base.enabled = false;
		}
		else if (this.Student.Dying)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.AlarmedEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.AlarmedEyes;
			}
		}
		else if (this.Student.Emetic || this.Student.Lethal || this.Student.Tranquil || this.Student.Headache)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.Textures[6])
			{
				this.MyRenderer.materials[1].mainTexture = this.Textures[6];
			}
		}
		else if (this.Student.Grudge)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.Textures[1])
			{
				this.MyRenderer.materials[1].mainTexture = this.Textures[1];
			}
		}
		else if (this.Student.LostTeacherTrust)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.SadEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.SadEyes;
			}
		}
		else if (this.Student.WitnessedMurder || this.Student.WitnessedCorpse)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.AlarmedEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.AlarmedEyes;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 2f)
			{
				while (this.ID == this.PreviousID)
				{
					this.ID = UnityEngine.Random.Range(0, this.Textures.Length);
				}
				this.MyRenderer.materials[1].mainTexture = this.Textures[this.ID];
				this.PreviousID = this.ID;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x040017ED RID: 6125
	public StudentScript Student;

	// Token: 0x040017EE RID: 6126
	public Renderer MyRenderer;

	// Token: 0x040017EF RID: 6127
	public Texture AlarmedEyes;

	// Token: 0x040017F0 RID: 6128
	public Texture DeadEyes;

	// Token: 0x040017F1 RID: 6129
	public Texture SadEyes;

	// Token: 0x040017F2 RID: 6130
	public Texture[] Textures;

	// Token: 0x040017F3 RID: 6131
	public float Timer;

	// Token: 0x040017F4 RID: 6132
	public int PreviousID;

	// Token: 0x040017F5 RID: 6133
	public int ID;
}
