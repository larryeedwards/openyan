using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000322 RID: 802
public class AntiCheatScript : MonoBehaviour
{
	// Token: 0x060016FC RID: 5884 RVA: 0x000B19DC File Offset: 0x000AFDDC
	private void Update()
	{
		if (this.Check && !base.GetComponent<AudioSource>().isPlaying)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x000B1A16 File Offset: 0x000AFE16
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YandereChan")
		{
			this.Jukebox.SetActive(false);
			this.Check = true;
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x04001612 RID: 5650
	public GameObject Jukebox;

	// Token: 0x04001613 RID: 5651
	public bool Check;
}
