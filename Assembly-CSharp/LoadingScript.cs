using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000452 RID: 1106
public class LoadingScript : MonoBehaviour
{
	// Token: 0x06001D7B RID: 7547 RVA: 0x001159D7 File Offset: 0x00113DD7
	private void Start()
	{
		SceneManager.LoadScene("SchoolScene");
	}
}
