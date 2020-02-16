using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class DemonSlashScript : MonoBehaviour
{
	// Token: 0x060018C5 RID: 6341 RVA: 0x000DFE51 File Offset: 0x000DE251
	private void Start()
	{
		this.MyAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x060018C6 RID: 6342 RVA: 0x000DFE60 File Offset: 0x000DE260
	private void Update()
	{
		if (this.MyCollider.enabled)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 0.333333343f)
			{
				this.MyCollider.enabled = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x060018C7 RID: 6343 RVA: 0x000DFEB8 File Offset: 0x000DE2B8
	private void OnTriggerEnter(Collider other)
	{
		Transform root = other.gameObject.transform.root;
		StudentScript component = root.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID != 1 && component.Alive)
		{
			component.DeathType = DeathType.EasterEgg;
			if (!component.Male)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.FemaleBloodyScream, root.transform.position + Vector3.up, Quaternion.identity);
			}
			else
			{
				UnityEngine.Object.Instantiate<GameObject>(this.MaleBloodyScream, root.transform.position + Vector3.up, Quaternion.identity);
			}
			component.BecomeRagdoll();
			component.Ragdoll.Dismember();
			this.MyAudio.Play();
		}
	}

	// Token: 0x04001C54 RID: 7252
	public GameObject FemaleBloodyScream;

	// Token: 0x04001C55 RID: 7253
	public GameObject MaleBloodyScream;

	// Token: 0x04001C56 RID: 7254
	public AudioSource MyAudio;

	// Token: 0x04001C57 RID: 7255
	public Collider MyCollider;

	// Token: 0x04001C58 RID: 7256
	public float Timer;
}
