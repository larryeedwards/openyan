using System;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class FoldedUniformScript : MonoBehaviour
{
	// Token: 0x060019A4 RID: 6564 RVA: 0x000EFC60 File Offset: 0x000EE060
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		bool flag = false;
		if (this.Spare && !GameGlobals.SpareUniform)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			flag = true;
		}
		if (!flag && this.Clean && this.Prompt.Button[0] != null)
		{
			this.Prompt.HideButton[0] = true;
			this.Yandere.StudentManager.NewUniforms++;
			this.Yandere.StudentManager.UpdateStudents(0);
			this.Yandere.StudentManager.Uniforms[this.Yandere.StudentManager.NewUniforms] = base.transform;
			Debug.Log("A new uniform has appeared. There are now " + this.Yandere.StudentManager.NewUniforms + " new uniforms at school.");
		}
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x000EFD58 File Offset: 0x000EE158
	private void Update()
	{
		if (this.Clean)
		{
			this.InPosition = this.Yandere.StudentManager.LockerRoomArea.bounds.Contains(base.transform.position);
			if (this.Yandere.MyRenderer.sharedMesh == this.Yandere.Towel)
			{
				Debug.Log("Yandere-chan is wearing a towel.");
			}
			if (this.Yandere.Bloodiness == 0f)
			{
				Debug.Log("Yandere-chan is not bloody.");
			}
			if (this.InPosition)
			{
				Debug.Log("This uniform is in the locker room.");
			}
			if (this.Yandere.MyRenderer.sharedMesh != this.Yandere.Towel || this.Yandere.Bloodiness != 0f || !this.InPosition)
			{
				this.Prompt.HideButton[0] = true;
			}
			else
			{
				this.Prompt.HideButton[0] = false;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.SteamCloud, this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
				this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_stripping_00");
				this.Yandere.CurrentUniformOrigin = 2;
				this.Yandere.Stripping = true;
				this.Yandere.CanMove = false;
				this.Timer += Time.deltaTime;
			}
			if (this.Timer > 0f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 1.5f)
				{
					this.Yandere.Schoolwear = 1;
					this.Yandere.ChangeSchoolwear();
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x04001EAA RID: 7850
	public YandereScript Yandere;

	// Token: 0x04001EAB RID: 7851
	public PromptScript Prompt;

	// Token: 0x04001EAC RID: 7852
	public GameObject SteamCloud;

	// Token: 0x04001EAD RID: 7853
	public bool InPosition = true;

	// Token: 0x04001EAE RID: 7854
	public bool Clean;

	// Token: 0x04001EAF RID: 7855
	public bool Spare;

	// Token: 0x04001EB0 RID: 7856
	public float Timer;

	// Token: 0x04001EB1 RID: 7857
	public int Type;
}
