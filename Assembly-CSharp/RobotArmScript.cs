using System;
using UnityEngine;

// Token: 0x020004D3 RID: 1235
public class RobotArmScript : MonoBehaviour
{
	// Token: 0x06001F47 RID: 8007 RVA: 0x0013F7FC File Offset: 0x0013DBFC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.ActivateArms();
		}
		if (this.Prompt.Circle[1].fillAmount == 0f)
		{
			this.ToggleWork();
		}
		if (this.UpdateArms)
		{
			if (this.On[0])
			{
				this.ArmValue[0] = Mathf.Lerp(this.ArmValue[0], 0f, Time.deltaTime * 5f);
				this.RobotArms.SetBlendShapeWeight(0, this.ArmValue[0]);
				if (this.ArmValue[0] < 0.1f)
				{
					this.RobotArms.SetBlendShapeWeight(0, 0f);
					this.UpdateArms = false;
					this.ArmValue[0] = 0f;
				}
			}
			else
			{
				this.ArmValue[0] = Mathf.Lerp(this.ArmValue[0], 100f, Time.deltaTime * 5f);
				this.RobotArms.SetBlendShapeWeight(0, this.ArmValue[0]);
				if (this.ArmValue[0] > 99.9f)
				{
					this.RobotArms.SetBlendShapeWeight(0, 100f);
					this.UpdateArms = false;
					this.ArmValue[0] = 100f;
				}
			}
		}
		if (this.Work)
		{
			if (this.StartWorkTimer > 0f)
			{
				this.ID = 1;
				while (this.ID < 9)
				{
					this.ArmValue[this.ID] = Mathf.Lerp(this.ArmValue[this.ID], 100f, Time.deltaTime * 5f);
					this.RobotArms.SetBlendShapeWeight(this.ID, this.ArmValue[this.ID]);
					this.ID += 2;
				}
				this.StartWorkTimer -= Time.deltaTime;
				if (this.StartWorkTimer < 0f)
				{
					this.ID = 1;
					while (this.ID < 9)
					{
						this.RobotArms.SetBlendShapeWeight(this.ID, 100f);
						this.ID += 2;
					}
				}
			}
			else
			{
				this.ID = 1;
				while (this.ID < 9)
				{
					this.Timer[this.ID] -= Time.deltaTime;
					if (this.Timer[this.ID] < 0f)
					{
						this.Sparks[this.ID].Stop();
						this.Sparks[this.ID + 1].Stop();
						int num = UnityEngine.Random.Range(0, 2);
						if (num == 1)
						{
							this.On[this.ID] = true;
						}
						else
						{
							this.On[this.ID] = false;
						}
						this.Timer[this.ID] = UnityEngine.Random.Range(1f, 2f);
					}
					if (this.On[this.ID])
					{
						this.ArmValue[this.ID] = Mathf.Lerp(this.ArmValue[this.ID], 0f, Time.deltaTime * 5f);
						this.ArmValue[this.ID + 1] = Mathf.Lerp(this.ArmValue[this.ID + 1], 100f, Time.deltaTime * 5f);
						this.RobotArms.SetBlendShapeWeight(this.ID, this.ArmValue[this.ID]);
						this.RobotArms.SetBlendShapeWeight(this.ID + 1, this.ArmValue[this.ID + 1]);
						if (this.ArmValue[this.ID] < 1f)
						{
							this.Sparks[this.ID].Play();
							this.RobotArms.SetBlendShapeWeight(this.ID, 0f);
							this.RobotArms.SetBlendShapeWeight(this.ID + 1, 100f);
							this.ArmValue[this.ID] = 0f;
							this.ArmValue[this.ID + 1] = 100f;
						}
					}
					else
					{
						this.ArmValue[this.ID] = Mathf.Lerp(this.ArmValue[this.ID], 100f, Time.deltaTime * 5f);
						this.ArmValue[this.ID + 1] = Mathf.Lerp(this.ArmValue[this.ID + 1], 0f, Time.deltaTime * 5f);
						this.RobotArms.SetBlendShapeWeight(this.ID, this.ArmValue[this.ID]);
						this.RobotArms.SetBlendShapeWeight(this.ID + 1, this.ArmValue[this.ID + 1]);
						if (this.ArmValue[this.ID] > 99f)
						{
							this.Sparks[this.ID + 1].Play();
							this.RobotArms.SetBlendShapeWeight(this.ID, 100f);
							this.RobotArms.SetBlendShapeWeight(this.ID + 1, 0f);
							this.ArmValue[this.ID] = 100f;
							this.ArmValue[this.ID + 1] = 0f;
						}
					}
					this.ID += 2;
				}
			}
		}
		else if (this.StopWorkTimer > 0f)
		{
			this.ID = 1;
			while (this.ID < 9)
			{
				this.ArmValue[this.ID] = Mathf.Lerp(this.ArmValue[this.ID], 0f, Time.deltaTime * 5f);
				this.RobotArms.SetBlendShapeWeight(this.ID, this.ArmValue[this.ID]);
				this.Sparks[this.ID].Stop();
				this.ID++;
			}
			this.StopWorkTimer -= Time.deltaTime;
			if (this.StopWorkTimer < 0f)
			{
				this.ID = 1;
				while (this.ID < 9)
				{
					this.RobotArms.SetBlendShapeWeight(this.ID, 0f);
					this.On[this.ID] = false;
					this.ID++;
				}
			}
		}
	}

	// Token: 0x06001F48 RID: 8008 RVA: 0x0013FE68 File Offset: 0x0013E268
	public void ActivateArms()
	{
		this.Prompt.Circle[0].fillAmount = 1f;
		this.UpdateArms = true;
		this.On[0] = !this.On[0];
		if (this.On[0])
		{
			this.Prompt.HideButton[1] = false;
			this.MyAudio.clip = this.ArmsOn;
		}
		else
		{
			this.Prompt.HideButton[1] = true;
			this.MyAudio.clip = this.ArmsOff;
			this.StopWorkTimer = 5f;
			this.Work = false;
		}
		this.MyAudio.Play();
	}

	// Token: 0x06001F49 RID: 8009 RVA: 0x0013FF13 File Offset: 0x0013E313
	public void ToggleWork()
	{
		this.Prompt.Circle[1].fillAmount = 1f;
		this.StartWorkTimer = 1f;
		this.StopWorkTimer = 5f;
		this.Work = !this.Work;
	}

	// Token: 0x04002A5B RID: 10843
	public SkinnedMeshRenderer RobotArms;

	// Token: 0x04002A5C RID: 10844
	public AudioSource MyAudio;

	// Token: 0x04002A5D RID: 10845
	public PromptScript Prompt;

	// Token: 0x04002A5E RID: 10846
	public Transform TerminalTarget;

	// Token: 0x04002A5F RID: 10847
	public ParticleSystem[] Sparks;

	// Token: 0x04002A60 RID: 10848
	public AudioClip ArmsOff;

	// Token: 0x04002A61 RID: 10849
	public AudioClip ArmsOn;

	// Token: 0x04002A62 RID: 10850
	public float StartWorkTimer;

	// Token: 0x04002A63 RID: 10851
	public float StopWorkTimer;

	// Token: 0x04002A64 RID: 10852
	public float[] ArmValue;

	// Token: 0x04002A65 RID: 10853
	public float[] Timer;

	// Token: 0x04002A66 RID: 10854
	public bool UpdateArms;

	// Token: 0x04002A67 RID: 10855
	public bool Work;

	// Token: 0x04002A68 RID: 10856
	public bool[] On;

	// Token: 0x04002A69 RID: 10857
	public int ID;
}
