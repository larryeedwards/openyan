using System;
using UnityEngine;

// Token: 0x0200044F RID: 1103
public class LiquidColliderScript : MonoBehaviour
{
	// Token: 0x06001D71 RID: 7537 RVA: 0x00114328 File Offset: 0x00112728
	private void Start()
	{
		if (this.Bucket)
		{
			base.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 400f);
		}
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x00114350 File Offset: 0x00112750
	private void Update()
	{
		if (!this.Static)
		{
			if (!this.Bucket)
			{
				if (base.transform.position.y < 0f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.GroundSplash, new Vector3(base.transform.position.x, 0f, base.transform.position.z), Quaternion.identity);
					this.NewPool = UnityEngine.Object.Instantiate<GameObject>(this.Pool, new Vector3(base.transform.position.x, 0.012f, base.transform.position.z), Quaternion.identity);
					this.NewPool.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
					if (this.Blood)
					{
						this.NewPool.transform.parent = GameObject.Find("BloodParent").transform;
					}
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			else
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x + Time.deltaTime * 2f, base.transform.localScale.y + Time.deltaTime * 2f, base.transform.localScale.z + Time.deltaTime * 2f);
			}
		}
	}

	// Token: 0x06001D73 RID: 7539 RVA: 0x001144F0 File Offset: 0x001128F0
	private void OnTriggerEnter(Collider other)
	{
		if (!this.AlreadyDoused && other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				if (!component.BeenSplashed && component.StudentID > 1 && component.StudentID != 10 && !component.Teacher && component.Club != ClubType.Council && !component.Fleeing && component.CurrentAction != StudentActionType.Sunbathe)
				{
					AudioSource.PlayClipAtPoint(this.SplashSound, base.transform.position);
					UnityEngine.Object.Instantiate<GameObject>(this.Splash, new Vector3(base.transform.position.x, 1.5f, base.transform.position.z), Quaternion.identity);
					if (this.Blood)
					{
						component.Bloody = true;
					}
					else if (this.Gas)
					{
						component.Gas = true;
					}
					component.GetWet();
					this.AlreadyDoused = true;
					UnityEngine.Object.Destroy(base.gameObject);
				}
				else if (!component.Wet && !component.Fleeing)
				{
					Debug.Log(component.Name + " just dodged a bucket of water.");
					if (component.Investigating)
					{
						component.StopInvestigating();
					}
					if (component.ReturningMisplacedWeapon)
					{
						component.DropMisplacedWeapon();
					}
					component.CharacterAnimation.CrossFade(component.DodgeAnim);
					component.Pathfinding.canSearch = false;
					component.Pathfinding.canMove = false;
					component.Routine = false;
					component.DodgeSpeed = 2f;
					component.Dodging = true;
					if (component.Following)
					{
						component.Hearts.emission.enabled = false;
						component.Yandere.Followers--;
						component.Following = false;
						component.CurrentDestination = component.Destinations[component.Phase];
						component.Pathfinding.target = component.Destinations[component.Phase];
						component.Pathfinding.speed = 1f;
					}
				}
			}
		}
	}

	// Token: 0x04002498 RID: 9368
	private GameObject NewPool;

	// Token: 0x04002499 RID: 9369
	public AudioClip SplashSound;

	// Token: 0x0400249A RID: 9370
	public GameObject GroundSplash;

	// Token: 0x0400249B RID: 9371
	public GameObject Splash;

	// Token: 0x0400249C RID: 9372
	public GameObject Pool;

	// Token: 0x0400249D RID: 9373
	public bool AlreadyDoused;

	// Token: 0x0400249E RID: 9374
	public bool Static;

	// Token: 0x0400249F RID: 9375
	public bool Bucket;

	// Token: 0x040024A0 RID: 9376
	public bool Blood;

	// Token: 0x040024A1 RID: 9377
	public bool Gas;
}
