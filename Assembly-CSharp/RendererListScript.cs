using System;
using UnityEngine;

// Token: 0x020004BD RID: 1213
public class RendererListScript : MonoBehaviour
{
	// Token: 0x06001F17 RID: 7959 RVA: 0x0013D76C File Offset: 0x0013BB6C
	private void Start()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		int num = 0;
		foreach (Transform transform in componentsInChildren)
		{
			if (transform.gameObject.GetComponent<Renderer>() != null)
			{
				this.Renderers[num] = transform.gameObject.GetComponent<Renderer>();
				num++;
			}
		}
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x0013D7D4 File Offset: 0x0013BBD4
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			foreach (Renderer renderer in this.Renderers)
			{
				renderer.enabled = !renderer.enabled;
			}
		}
	}

	// Token: 0x04002992 RID: 10642
	public Renderer[] Renderers;
}
