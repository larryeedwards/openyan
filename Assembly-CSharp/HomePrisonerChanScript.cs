using System;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class HomePrisonerChanScript : MonoBehaviour
{
	// Token: 0x06001CBC RID: 7356 RVA: 0x00106ED0 File Offset: 0x001052D0
	private void Start()
	{
		if (SchoolGlobals.KidnapVictim > 0)
		{
			this.StudentID = SchoolGlobals.KidnapVictim;
			if (StudentGlobals.GetStudentSanity(this.StudentID) == 100f)
			{
				this.AnkleRopes.SetActive(false);
			}
			this.PermanentAngleR = this.TwintailR.eulerAngles;
			this.PermanentAngleL = this.TwintailL.eulerAngles;
			if (!StudentGlobals.GetStudentArrested(this.StudentID) && !StudentGlobals.GetStudentDead(this.StudentID))
			{
				this.Cosmetic.StudentID = this.StudentID;
				this.Cosmetic.enabled = true;
				this.BreastSize = this.JSON.Students[this.StudentID].BreastSize;
				this.RightEyeRotOrigin = this.RightEye.localEulerAngles;
				this.LeftEyeRotOrigin = this.LeftEye.localEulerAngles;
				this.RightEyeOrigin = this.RightEye.localPosition;
				this.LeftEyeOrigin = this.LeftEye.localPosition;
				this.UpdateSanity();
				this.TwintailR.transform.localEulerAngles = new Vector3(0f, 180f, -90f);
				this.TwintailL.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
				this.Blindfold.SetActive(false);
				this.Tripod.SetActive(false);
				if (this.StudentID == 81 && !StudentGlobals.GetStudentBroken(81) && SchemeGlobals.GetSchemeStage(6) > 4)
				{
					this.Blindfold.SetActive(true);
					this.Tripod.SetActive(true);
				}
			}
			else
			{
				SchoolGlobals.KidnapVictim = 0;
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x001070A0 File Offset: 0x001054A0
	private void LateUpdate()
	{
		this.Skirt.transform.localPosition = new Vector3(0f, -0.135f, 0.01f);
		this.Skirt.transform.localScale = new Vector3(this.Skirt.transform.localScale.x, 1.2f, this.Skirt.transform.localScale.z);
		if (!this.Tortured)
		{
			if (this.Sanity > 0f)
			{
				if (this.LookAhead)
				{
					this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x - 45f, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
				}
				else if (this.YandereDetector.YandereDetected && Vector3.Distance(base.transform.position, this.HomeYandere.position) < 2f)
				{
					Quaternion b;
					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						b = Quaternion.LookRotation(this.HomeCamera.transform.position + Vector3.down * (1.5f * ((100f - this.Sanity) / 100f)) - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot1, Time.deltaTime * 2f);
					}
					else
					{
						b = Quaternion.LookRotation(this.HomeYandere.position + Vector3.up * 1.5f - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot2, Time.deltaTime * 2f);
					}
					this.Neck.rotation = Quaternion.Slerp(this.LastRotation, b, Time.deltaTime * 2f);
					this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
					this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
				}
				else
				{
					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						Quaternion b2 = Quaternion.LookRotation(this.HomeCamera.transform.position + Vector3.down * (1.5f * ((100f - this.Sanity) / 100f)) - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot3, Time.deltaTime * 2f);
					}
					else
					{
						Quaternion b2 = Quaternion.LookRotation(base.transform.position + base.transform.forward - this.Neck.position);
						this.Neck.rotation = Quaternion.Slerp(this.LastRotation, b2, Time.deltaTime * 2f);
					}
					this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot4, Time.deltaTime * 2f);
					this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
					this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
				}
			}
			else
			{
				this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x - 45f, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
			}
		}
		this.LastRotation = this.Neck.rotation;
		if (!this.Tortured && this.Sanity < 100f && this.Sanity > 0f)
		{
			this.TwitchTimer += Time.deltaTime;
			if (this.TwitchTimer > this.NextTwitch)
			{
				this.Twitch = new Vector3((1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f));
				this.NextTwitch = UnityEngine.Random.Range(0f, 1f);
				this.TwitchTimer = 0f;
			}
			this.Twitch = Vector3.Lerp(this.Twitch, Vector3.zero, Time.deltaTime * 10f);
			this.Neck.localEulerAngles += this.Twitch;
		}
		if (this.Tortured)
		{
			this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot5, Time.deltaTime * 2f);
			this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
			this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
		}
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x0010768C File Offset: 0x00105A8C
	public void UpdateSanity()
	{
		this.Sanity = StudentGlobals.GetStudentSanity(this.StudentID);
		bool active = this.Sanity == 0f;
		this.RightMindbrokenEye.SetActive(active);
		this.LeftMindbrokenEye.SetActive(active);
	}

	// Token: 0x04002214 RID: 8724
	public HomeYandereDetectorScript YandereDetector;

	// Token: 0x04002215 RID: 8725
	public HomeCameraScript HomeCamera;

	// Token: 0x04002216 RID: 8726
	public CosmeticScript Cosmetic;

	// Token: 0x04002217 RID: 8727
	public JsonScript JSON;

	// Token: 0x04002218 RID: 8728
	public Vector3 RightEyeRotOrigin;

	// Token: 0x04002219 RID: 8729
	public Vector3 LeftEyeRotOrigin;

	// Token: 0x0400221A RID: 8730
	public Vector3 PermanentAngleR;

	// Token: 0x0400221B RID: 8731
	public Vector3 PermanentAngleL;

	// Token: 0x0400221C RID: 8732
	public Vector3 RightEyeOrigin;

	// Token: 0x0400221D RID: 8733
	public Vector3 LeftEyeOrigin;

	// Token: 0x0400221E RID: 8734
	public Vector3 Twitch;

	// Token: 0x0400221F RID: 8735
	public Quaternion LastRotation;

	// Token: 0x04002220 RID: 8736
	public Transform HomeYandere;

	// Token: 0x04002221 RID: 8737
	public Transform RightBreast;

	// Token: 0x04002222 RID: 8738
	public Transform LeftBreast;

	// Token: 0x04002223 RID: 8739
	public Transform TwintailR;

	// Token: 0x04002224 RID: 8740
	public Transform TwintailL;

	// Token: 0x04002225 RID: 8741
	public Transform RightEye;

	// Token: 0x04002226 RID: 8742
	public Transform LeftEye;

	// Token: 0x04002227 RID: 8743
	public Transform Skirt;

	// Token: 0x04002228 RID: 8744
	public Transform Neck;

	// Token: 0x04002229 RID: 8745
	public GameObject RightMindbrokenEye;

	// Token: 0x0400222A RID: 8746
	public GameObject LeftMindbrokenEye;

	// Token: 0x0400222B RID: 8747
	public GameObject AnkleRopes;

	// Token: 0x0400222C RID: 8748
	public GameObject Blindfold;

	// Token: 0x0400222D RID: 8749
	public GameObject Character;

	// Token: 0x0400222E RID: 8750
	public GameObject Tripod;

	// Token: 0x0400222F RID: 8751
	public float HairRotation;

	// Token: 0x04002230 RID: 8752
	public float TwitchTimer;

	// Token: 0x04002231 RID: 8753
	public float NextTwitch;

	// Token: 0x04002232 RID: 8754
	public float BreastSize;

	// Token: 0x04002233 RID: 8755
	public float EyeShrink;

	// Token: 0x04002234 RID: 8756
	public float Sanity;

	// Token: 0x04002235 RID: 8757
	public float HairRot1;

	// Token: 0x04002236 RID: 8758
	public float HairRot2;

	// Token: 0x04002237 RID: 8759
	public float HairRot3;

	// Token: 0x04002238 RID: 8760
	public float HairRot4;

	// Token: 0x04002239 RID: 8761
	public float HairRot5;

	// Token: 0x0400223A RID: 8762
	public bool LookAhead;

	// Token: 0x0400223B RID: 8763
	public bool Tortured;

	// Token: 0x0400223C RID: 8764
	public bool Male;

	// Token: 0x0400223D RID: 8765
	public int StudentID;
}
