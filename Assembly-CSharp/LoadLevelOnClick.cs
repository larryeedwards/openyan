using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000198 RID: 408
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x06000C6E RID: 3182 RVA: 0x0006811F File Offset: 0x0006651F
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			SceneManager.LoadScene(this.levelName);
		}
	}

	// Token: 0x04000B0B RID: 2827
	public string levelName;
}
